using Newtonsoft.Json;
using System.Collections.Generic;

namespace BetfairNG.Data
{
    public class UpdateExecutionReport
    {
        [JsonProperty(PropertyName = "customerRef")]
        public string CustomerRef { get; set; }

        [JsonProperty(PropertyName = "errorCode")]
        public InstructionReportErrorCode ErrorCode { get; set; }

        [JsonProperty(PropertyName = "instructionReports")]
        public IList<UpdateInstructionReport> InstructionReports { get; set; }

        [JsonProperty(PropertyName = "marketId")]
        public string MarketId { get; set; }

        [JsonProperty(PropertyName = "status")]
        public InstructionReportStatus Status { get; set; }
    }
}