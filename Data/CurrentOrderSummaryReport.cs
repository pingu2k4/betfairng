using Newtonsoft.Json;
using System.Collections.Generic;

namespace BetfairNG.Data
{
    public class CurrentOrderSummaryReport
    {
        [JsonProperty(PropertyName = "currentOrders")]
        public IList<CurrentOrderSummary> CurrentOrders { get; set; }

        [JsonProperty(PropertyName = "moreAvailable")]
        public bool MoreAvailable { get; set; }
    }
}