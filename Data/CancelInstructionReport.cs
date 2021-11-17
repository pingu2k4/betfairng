using Newtonsoft.Json;
using System;

namespace BetfairNG.Data
{
    public class CancelInstructionReport
    {
        [JsonProperty(PropertyName = "cancelledDate")]
        public DateTime CancelledDate { get; set; }

        [JsonProperty(PropertyName = "errorCode")]
        public InstructionReportErrorCode ErrorCode { get; set; }

        [JsonProperty(PropertyName = "instruction")]
        public CancelInstruction Instruction { get; set; }

        [JsonProperty(PropertyName = "sizeCancelled")]
        public double SizeCancelled { get; set; }

        [JsonProperty(PropertyName = "status")]
        public InstructionReportStatus Status { get; set; }
    }
}