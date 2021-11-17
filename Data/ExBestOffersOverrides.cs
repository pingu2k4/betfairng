using Newtonsoft.Json;

namespace BetfairNG.Data
{
    public class ExBestOffersOverrides
    {
        [JsonProperty(PropertyName = "bestPricesDepth")]
        public int BestPricesDepth { get; set; }

        [JsonProperty(PropertyName = "rollupLiabilityFactor")]
        public int RollUpLiabilityFactor { get; set; }

        [JsonProperty(PropertyName = "rollupLiabilityThreshold")]
        public double RollUpLiabilityThreshold { get; set; }

        [JsonProperty(PropertyName = "rollupLimit")]
        public int RollUpLimit { get; set; }

        [JsonProperty(PropertyName = "rollupModel")]
        public RollUpModel RollUpModel { get; set; }
    }
}