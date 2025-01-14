using Newtonsoft.Json;
using System;
using System.Text;

namespace BetfairNG.Data
{
    public class PlaceInstructionReport
    {
        [JsonProperty(PropertyName = "averagePriceMatched")]
        public double? AveragePriceMatched { get; set; }

        [JsonProperty(PropertyName = "betId")]
        public string BetId { get; set; }

        [JsonProperty(PropertyName = "errorCode")]
        public InstructionReportErrorCode ErrorCode { get; set; }

        [JsonProperty(PropertyName = "instruction")]
        public PlaceInstruction Instruction { get; set; }

        [JsonProperty(PropertyName = "placedDate")]
        public DateTime? PlacedDate { get; set; }

        [JsonProperty(PropertyName = "sizeMatched")]
        public double? SizeMatched { get; set; }

        [JsonProperty(PropertyName = "status")]
        public InstructionReportStatus Status { get; set; }

        public override string ToString()
        {
            return new StringBuilder()
                        .AppendFormat("Status={0}", Status)
                        .AppendFormat(" : ErrorCode={0}", ErrorCode)
                        .AppendFormat(" : Instruction={{{0}}}", Instruction)
                        .AppendFormat(" : BetId={0}", BetId)
                        .AppendFormat(" : PlacedDate={0}", PlacedDate)
                        .AppendFormat(" : AveragePriceMatched={0}", AveragePriceMatched)
                        .AppendFormat(" : SizeMatched={0}", SizeMatched)
                        .ToString();
        }
    }
}