using NetnsAgent.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;

namespace NetnsAgent
{
    class Program
    {
        static void Main(string[] args)
        {
            // var output = "ip -j -n red link".Bash();

            var output = "ip -j -n red addr".Bash();

            var links = JsonConvert.DeserializeObject<List<IPLink>>(output);

            //Console.WriteLine(output);
            foreach(var link in links)
            {
                Console.WriteLine($"# {link.Ifname} {link.Ifindex} {link.Link} {link.Linkmode} {link.Address} {link.Broadcast}");

                if(link.AddrInfo != null) {
                    foreach(var addr in link.AddrInfo) {
                        Console.WriteLine($"{addr.Label} = {addr.Local}");
                    }
                }
            }
            // Console.WriteLine(output);
        }
    }
}
