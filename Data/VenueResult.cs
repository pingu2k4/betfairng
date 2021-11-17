using Newtonsoft.Json;

namespace BetfairNG.Data
{
    public class VenueResult
    {
        [JsonProperty(PropertyName = "marketCount")]
        public int MarketCount { get; set; }

        [JsonProperty(PropertyName = "venue")]
        public string Venue { get; set; }
    }
}