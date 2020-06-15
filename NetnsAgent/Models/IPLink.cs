using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetnsAgent.Models
{
    public partial class IPLink
    {
        [JsonProperty("ifindex")]
        public long Ifindex { get; set; }

        [JsonProperty("ifname")]
        public string Ifname { get; set; }

        [JsonProperty("flags")]
        public string[] Flags { get; set; }

        [JsonProperty("mtu")]
        public long Mtu { get; set; }

        [JsonProperty("qdisc")]
        public string Qdisc { get; set; }

        [JsonProperty("operstate")]
        public string Operstate { get; set; }

        [JsonProperty("linkmode")]
        public string Linkmode { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("txqlen")]
        public long Txqlen { get; set; }

        [JsonProperty("link_type")]
        public string LinkType { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("broadcast")]
        public string Broadcast { get; set; }

        [JsonProperty("link")]
        public object Link { get; set; }
    }
}
