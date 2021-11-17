using BetfairNG;
using BetfairNG.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

namespace ConsoleExample
{
    public class StreamingExample
    {
        private readonly BetfairClient _client;
        private readonly ConcurrentQueue<MarketCatalogue> _markets = new();
        private readonly StreamingBetfairClient _streamingClient;

        public StreamingExample(BetfairClient client, StreamingBetfairClient streamingClient)
        {
            _client = client;
            _streamingClient = streamingClient;
        }

        public static bool IsBlocking
        { get { return true; } }

        public void Go()
        {
            // find all the upcoming UK horse races (EventTypeId 7)
            var marketFilter = new MarketFilter
            {
                EventTypeIds = new HashSet<string>() { "7" },
                MarketStartTime = new TimeRange()
                {
                    From = DateTime.Now,
                    To = DateTime.Now.AddDays(2)
                },
                MarketTypeCodes = new HashSet<string>() { "WIN" }
            };

            Console.WriteLine("BetfairClient.ListEvents()");
            var events = _client.ListEvents(marketFilter).Result;
            if (events.HasError)
                throw new ApplicationException();
            var firstEvent = events.Response.First();
            Console.WriteLine("First Event {0} {1}", firstEvent.Event.Id, firstEvent.Event.Name);

            var marketCatalogues = _client.ListMarketCatalogue(
              BFHelpers.HorseRaceFilter(),
              BFHelpers.HorseRaceProjection(),
              MarketSort.FIRST_TO_START,
              25).Result.Response;

            marketCatalogues.ForEach(c =>
            {
                _markets.Enqueue(c);
                Console.WriteLine(c.MarketName);
            });
            Console.WriteLine();

            while (!_markets.IsEmpty)
            {
                AutoResetEvent waitHandle = new(false);
                _markets.TryDequeue(out MarketCatalogue marketCatalogue);

                var marketSubscription = _streamingClient.SubscribeMarket(marketCatalogue.MarketId)
                    .SubscribeOn(Scheduler.Default)
                    .Subscribe(
                    tick =>
                    {
                        Console.WriteLine(BFHelpers.MarketSnapConsole(tick, marketCatalogue.Runners));
                    },
                    () =>
                    {
                        Console.WriteLine("Market finished");
                        waitHandle.Set();
                    });

                waitHandle.WaitOne();
                marketSubscription.Dispose();
            }
        }
    }
}