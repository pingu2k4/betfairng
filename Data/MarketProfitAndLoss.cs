using Newtonsoft.Json;
using System.Collections.Generic;

namespace BetfairNG.Data
{
    public class MarketProfitAndLoss
    {
        [JsonProperty(PropertyName = "commissionApplied")]
        public double CommissionApplied { get; set; }

        [JsonProperty(PropertyName = "marketId")]
        public string MarketId { get; set; }

        [JsonProperty(PropertyName = "profitAndLosses")]
        public List<RunnerProfitAndLoss> ProfitAndLosses { get; set; }
    }
}