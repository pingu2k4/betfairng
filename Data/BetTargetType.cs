﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BetfairNG.Data
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BetTargetType
    {
        BACKERS_PROFIT,
        PAYOUT,
    }
}