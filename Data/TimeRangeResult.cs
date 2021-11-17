using Newtonsoft.Json;

namespace BetfairNG.Data
{
    public class TimeRangeResult
    {
        [JsonProperty(PropertyName = "marketCount")]
        public int MarketCount { get; set; }

        [JsonProperty(PropertyName = "timeRange")]
        public TimeRange TimeRange { get; set; }
    }
}