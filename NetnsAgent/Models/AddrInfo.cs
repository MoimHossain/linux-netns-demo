using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetnsAgent.Models
{
    public partial class AddrInfo
    {
        [JsonProperty("family")]
        public string Family { get; set; }

        [JsonProperty("local")]
        public string Local { get; set; }

        [JsonProperty("prefixlen")]
        public int Prefixlen { get; set; }

        [JsonProperty("metric")]
        public int? Metric { get; set; }

        [JsonProperty("broadcast")]
        public string Broadcast { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("valid_life_time")]
        public long ValidLifeTime { get; set; }

        [JsonProperty("preferred_life_time")]
        public long PreferredLifeTime { get; set; }

        [JsonProperty("noprefixroute")]
        public bool? NoPrefixRoute { get; set; }
    }
}