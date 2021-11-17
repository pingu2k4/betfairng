using Betfair.ESAClient;
using Betfair.ESAClient.Auth;
using Betfair.ESAClient.Cache;
using Betfair.ESAClient.Protocol;
using Betfair.ESASwagger.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace BetfairNG
{
    /// <summary>
    /// Streaming Betfair Client with caching
    /// </summary>
    public class StreamingBetfairClient : IChangeMessageHandler
    {
        private static readonly TraceSource trace = new TraceSource("StreamingBetfairClient");
        private readonly string appKey;
        private readonly MarketCache marketCache = new MarketCache();

        private readonly ConcurrentDictionary<string, IObserver<MarketSnap>> marketObservers =
            new ConcurrentDictionary<string, IObserver<MarketSnap>>();

        private readonly ConcurrentDictionary<string, IObservable<MarketSnap>> marketsObservables =
            new ConcurrentDictionary<string, IObservable<MarketSnap>>();

        private readonly OrderCache orderCache = new OrderCache();

        private readonly ConcurrentDictionary<string, IObservable<OrderMarketSnap>> orderObservables =
            new ConcurrentDictionary<string, IObservable<OrderMarketSnap>>();

        private readonly ConcurrentDictionary<string, IObserver<OrderMarketSnap>> orderObservers =
            new ConcurrentDictionary<string, IObserver<OrderMarketSnap>>();

        private readonly Action preNetworkRequest;
        private readonly string streamEndPointHostName;
        private Client networkClient;

        public StreamingBetfairClient(
            string streamEndPointHostName,
            string appKey,
            Action preNetworkRequest = null)
        {
            if (string.IsNullOrWhiteSpace(streamEndPointHostName)) throw new ArgumentException("streamEndPointHostName");
            if (string.IsNullOrWhiteSpace(appKey)) throw new ArgumentException("appKey");

            this.streamEndPointHostName = streamEndPointHostName;
            this.appKey = appKey;
            this.preNetworkRequest = preNetworkRequest;

            this.marketCache.MarketChanged += MarketCache_MarketChanged;
            this.orderCache.OrderMarketChanged += OrderCache_OrderMarketChanged;
        }

        public long? ConflatMs
        {
            get
            {
                return networkClient.ConflateMs;
            }
            set
            {
                networkClient.ConflateMs = value;
            }
        }

        public ConnectionStatus Status
        {
            get
            {
                return networkClient.Status;
            }
        }

        public bool Login(string username, string password, string ssoHostName = "identitysso.betfair.com")
        {
            AppKeyAndSessionProvider authProvider = new AppKeyAndSessionProvider(ssoHostName, appKey, username, password);
            networkClient = new Client(streamEndPointHostName, 443, authProvider)
            {
                ChangeHandler = this
            };
            networkClient.Start();

            return true;
        }

        public void OnErrorStatusNotification(StatusMessage message)
        {
            // TODO:// sort this out
        }

        public void OnMarketChange(ChangeMessage<MarketChange> changeMessage)
        {
            marketCache.OnMarketChange(changeMessage);
        }

        public void OnOrderChange(ChangeMessage<OrderMarketChange> changeMessage)
        {
            orderCache.OnOrderChange(changeMessage);
        }

        public IObservable<MarketSnap> SubscribeMarket(string marketId)
        {
            MarketFilter filter = new MarketFilter { MarketIds = new List<string>() { marketId } };
            MarketSubscriptionMessage message = new MarketSubscriptionMessage() { MarketFilter = filter };
            return SubscribeMarket(marketId, message);
        }

        public IObservable<MarketSnap> SubscribeMarket(string marketId, MarketSubscriptionMessage message)
        {
            networkClient.Start();

            if (marketsObservables.TryGetValue(marketId, out IObservable<MarketSnap> market))
            {
                networkClient.MarketSubscription(message);
                return market;
            }

            var observable = Observable.Create<MarketSnap>(
               (IObserver<MarketSnap> observer) =>
               {
                   marketObservers.AddOrUpdate(marketId, observer, (key, existingVal) => existingVal);
                   return Disposable.Create(() =>
                   {
                       marketsObservables.TryRemove(marketId, out IObservable<MarketSnap> o);
                       marketObservers.TryRemove(marketId, out IObserver<MarketSnap> ob);
                   });
               })
               .Publish()
               .RefCount();

            marketsObservables.AddOrUpdate(marketId, observable, (key, existingVal) => existingVal);

            // TODO:// race?
            networkClient.MarketSubscription(message);
            return observable;
        }

        public IObservable<OrderMarketSnap> SubscribeOrders(string marketId)
        {
            OrderSubscriptionMessage orderSubscription = new OrderSubscriptionMessage();
            return SubscribeOrders(marketId, orderSubscription);
        }

        public IObservable<OrderMarketSnap> SubscribeOrders(string marketId, OrderSubscriptionMessage orderSubscription)
        {
            networkClient.Start();

            if (orderObservables.TryGetValue(marketId, out IObservable<OrderMarketSnap> orderObservable))
            {
                networkClient.OrderSubscription(orderSubscription);
                return orderObservable;
            }

            var observable = Observable.Create<OrderMarketSnap>(
               (IObserver<OrderMarketSnap> observer) =>
               {
                   orderObservers.AddOrUpdate(marketId, observer, (key, existingVal) => existingVal);

                   return Disposable.Create(() =>
                   {
                       orderObservables.TryRemove(marketId, out IObservable<OrderMarketSnap> o);
                       orderObservers.TryRemove(marketId, out IObserver<OrderMarketSnap> ob);
                   });
               })
               .Publish()
               .RefCount();

            orderObservables.AddOrUpdate(marketId, observable, (key, existingVal) => existingVal);

            // TODO:// race?
            networkClient.OrderSubscription(orderSubscription);
            return observable;
        }

        private void MarketCache_MarketChanged(object sender, MarketChangedEventArgs e)
        {
            if (marketObservers.TryGetValue(e.Market.MarketId, out IObserver<MarketSnap> o))
            {
                // check to see if the market is finished
                if (e.Market.IsClosed)
                    o.OnCompleted();
                else
                    o.OnNext(e.Snap);
            }
        }

        private void OrderCache_OrderMarketChanged(object sender, OrderMarketChangedEventArgs e)
        {
            if (orderObservers.TryGetValue(e.Snap.MarketId, out IObserver<OrderMarketSnap> o))
            {
                // check to see if the market is finished
                if (e.Snap.IsClosed)
                    o.OnCompleted();
                else
                    o.OnNext(e.Snap);
            }
        }
    }
}