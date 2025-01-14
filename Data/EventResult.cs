using Newtonsoft.Json;
using System.Text;

namespace BetfairNG.Data
{
    public class EventResult
    {
        [JsonProperty(PropertyName = "event")]
        public Event Event { get; set; }

        [JsonProperty(PropertyName = "marketCount")]
        public int MarketCount { get; set; }

        public override string ToString()
        {
            return new StringBuilder().AppendFormat("{0}", "EventResult")
                        .AppendFormat(" : {0}", Event)
                        .AppendFormat(" : MarketCount={0}", MarketCount)
                        .ToString();
        }
    }
}