using Newtonsoft.Json;
using System.Text;

namespace BetfairNG.Data
{
    public class LimitOnCloseOrder
    {
        [JsonProperty(PropertyName = "liability")]
        public double Liability { get; set; }

        [JsonProperty(PropertyName = "size")]
        public double Size { get; set; }

        public override string ToString()
        {
            return new StringBuilder()
                        .AppendFormat("Size={0}", Size)
                        .AppendFormat(" : Liability={0}", Liability)
                        .ToString();
        }
    }
}