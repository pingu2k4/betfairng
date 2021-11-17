using System.Collections.Generic;

namespace Betfair.ESAClient.Cache
{
    /// <summary>
    /// Atomic snap of the prices associated with a runner.
    /// </summary>
    public class MarketRunnerPrices
    {
        public static readonly MarketRunnerPrices EMPTY = new()
        {
            AvailableToLay = PriceSize.EmptyList,
            AvailableToBack = PriceSize.EmptyList,
            Traded = PriceSize.EmptyList,
            StartingPriceBack = PriceSize.EmptyList,
            StartingPriceLay = PriceSize.EmptyList,

            BestAvailableToBack = LevelPriceSize.EmptyList,
            BestAvailableToLay = LevelPriceSize.EmptyList,
            BestDisplayAvailableToBack = LevelPriceSize.EmptyList,
            BestDisplayAvailableToLay = LevelPriceSize.EmptyList,
        };

        public IList<PriceSize> AvailableToBack { get; internal set; }

        public IList<PriceSize> AvailableToLay { get; internal set; }

        public IList<LevelPriceSize> BestAvailableToBack { get; internal set; }

        public IList<LevelPriceSize> BestAvailableToLay { get; internal set; }

        public IList<LevelPriceSize> BestDisplayAvailableToBack { get; internal set; }

        public IList<LevelPriceSize> BestDisplayAvailableToLay { get; internal set; }

        public double LastTradedPrice { get; internal set; }

        public IList<PriceSize> StartingPriceBack { get; internal set; }

        public double StartingPriceFar { get; internal set; }

        public IList<PriceSize> StartingPriceLay { get; internal set; }

        public double StartingPriceNear { get; internal set; }

        public IList<PriceSize> Traded { get; internal set; }

        public double TradedVolume { get; internal set; }

        public override string ToString()
        {
            return "MarketRunnerPrices{" +
                "AvailableToLay=" + string.Join(", ", AvailableToLay) +
                ", AvailableToBack=" + string.Join(", ", AvailableToBack) +
                ", Traded=" + string.Join(", ", Traded) +
                ", StartingPriceBack=" + string.Join(", ", StartingPriceBack) +
                ", StartingPriceLay=" + string.Join(", ", StartingPriceLay) +

                ", BestAvailableToBack=" + string.Join(", ", BestAvailableToBack) +
                ", BestAvailableToLay=" + string.Join(", ", BestAvailableToLay) +
                ", BestDisplayAvailableToBack=" + string.Join(", ", BestDisplayAvailableToBack) +
                ", BestDisplayAvailableToLay=" + string.Join(", ", BestDisplayAvailableToLay) +

                ", LastTradedPrice=" + LastTradedPrice +
                ", StartingPriceNear=" + StartingPriceNear +
                ", StartingPriceFar=" + StartingPriceFar +
                ", TradedVolume=" + TradedVolume +
                "}";
        }
    }
}