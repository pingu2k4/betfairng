using Newtonsoft.Json;
using System.Collections.Generic;

namespace BetfairNG.Data
{
    public class PriceProjection
    {
        [JsonProperty(PropertyName = "exBestOffersOverrides")]
        public ExBestOffersOverrides ExBestOffersOverrides { get; set; }

        [JsonProperty(PropertyName = "priceData")]
        public ISet<PriceData> PriceData { get; set; }

        [JsonProperty(PropertyName = "rolloverStakes")]
        public bool? RolloverStakes { get; set; }

        [JsonProperty(PropertyName = "virtualise")]
        public bool? Virtualise { get; set; }
    }
}