using Newtonsoft.Json;

namespace BetfairNG.Data
{
    public class RunnerId
    {
        [JsonProperty(PropertyName = "handicap")]
        public double Handicap { get; set; }

        [JsonProperty(PropertyName = "marketId")]
        public string MarketId { get; set; }

        [JsonProperty(PropertyName = "selectionId")]
        public long SelectionId { get; set; }
    }
}