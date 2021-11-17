using Newtonsoft.Json;

namespace BetfairNG.Data
{
    public class UpdateInstructionReport
    {
        [JsonProperty(PropertyName = "errorCode")]
        public InstructionReportErrorCode ErrorCode { get; set; }

        [JsonProperty(PropertyName = "instruction")]
        public UpdateInstruction Instruction { get; set; }

        [JsonProperty(PropertyName = "status")]
        public InstructionReportStatus Status { get; set; }
    }
}