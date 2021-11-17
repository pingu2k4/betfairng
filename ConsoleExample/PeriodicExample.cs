using BetfairNG;
using BetfairNG.Data;
using System;
using System.Collections.Concurrent;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace ConsoleExample
{
    public class PeriodicExample : IDisposable
    {
        private readonly MarketListenerPeriodic _marketListener;
        private readonly ConcurrentQueue<MarketCatalogue> _markets = new();
        private IDisposable _marketSubscription;

        public PeriodicExample(BetfairClient client, double pollIntervalInSeconds)
        {
            var betfairClient = client;

            var marketCatalogues = betfairClient.ListMarketCatalogue(
                BFHelpers.HorseRaceFilter("GB"),
                BFHelpers.HorseRaceProjection(),
                MarketSort.FIRST_TO_START,
                25).Result.Response;

            marketCatalogues.ForEach(c =>
            {
                _markets.Enqueue(c);
            });

            _marketListener = MarketListenerPeriodic.Create(betfairClient
                                                            , BFHelpers.HorseRacePriceProjection()
                                                            , pollIntervalInSeconds);
        }

        public static bool IsBlocking
        { get { return false; } }

        public void Dispose()
        {
            _marketSubscription.Dispose();
        }

        public void Go()
        {
            _markets.TryDequeue(out MarketCatalogue marketCatalogue);

            _marketSubscription = _marketListener.SubscribeMarketBook(marketCatalogue.MarketId)
                .SubscribeOn(Scheduler.Default)
                .Subscribe(
                    tick =>
                    {
                        Console.WriteLine(BFHelpers.MarketBookConsole(marketCatalogue, tick, marketCatalogue.Runners));
                    },
                    () =>
                    {
                        Console.WriteLine("Market finished");
                    });
        }
    }
}