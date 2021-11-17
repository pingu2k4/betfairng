using Newtonsoft.Json;

namespace BetfairNG.Data
{
    public class MarketVersion
    {
        [JsonProperty(PropertyName = "version")]
        public long Version { get; set; }
    }
}