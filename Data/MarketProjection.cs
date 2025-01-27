using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BetfairNG.Data
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MarketProjection
    {
        COMPETITION,
        EVENT,
        EVENT_TYPE,
        MARKET_DESCRIPTION,
        MARKET_START_TIME,
        RUNNER_DESCRIPTION,
        RUNNER_METADATA
    }
}