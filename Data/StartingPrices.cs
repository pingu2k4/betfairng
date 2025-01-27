using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace BetfairNG.Data
{
    public class StartingPrices
    {
        [JsonProperty(PropertyName = "actualSP")]
        public double ActualSP { get; set; }

        [JsonProperty(PropertyName = "backStakeTaken")]
        public List<PriceSize> BackStakeTaken { get; set; }

        [JsonProperty(PropertyName = "farPrice")]
        public double FarPrice { get; set; }

        [JsonProperty(PropertyName = "layLiabilityTaken")]
        public List<PriceSize> LayLiabilityTaken { get; set; }

        [JsonProperty(PropertyName = "nearPrice")]
        public double NearPrice { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder().AppendFormat("{0}", "StartingPrices")
                        .AppendFormat(" : NearPrice={0}", NearPrice)
                        .AppendFormat(" : FarPrice={0}", FarPrice)
                        .AppendFormat(" : ActualSP={0}", ActualSP);

            if (BackStakeTaken != null && BackStakeTaken.Count > 0)
            {
                int idx = 0;
                foreach (var backStake in BackStakeTaken)
                {
                    sb.AppendFormat(" : BackStake[{0}]={1}", idx++, backStake);
                }
            }

            if (LayLiabilityTaken != null && LayLiabilityTaken.Count > 0)
            {
                int idx = 0;
                foreach (var layLiability in LayLiabilityTaken)
                {
                    sb.AppendFormat(" : LayLiability{0}]={1}", idx++, layLiability);
                }
            }

            return sb.ToString();
        }
    }
}