using Newtonsoft.Json;

namespace BetfairNG.Data
{
    public class MarketTypeResult
    {
        [JsonProperty(PropertyName = "marketCount")]
        public int MarketCount { get; set; }

        [JsonProperty(PropertyName = "marketType")]
        public string MarketType { get; set; }
    }
}