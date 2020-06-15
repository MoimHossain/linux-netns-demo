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
            var output = string.Empty;
            if(args != null && args.Length > 0)
            {
                output = string.Concat(args.Select(s=> string.Format($" {s}"))).Bash();

                var links = JsonConvert.DeserializeObject<List<IPLink>>(output);

                int lineNo = 0;
                foreach(var link in links)
                {
                    Console.WriteLine($"Line no: {link.Ifname} {link.Ifindex} {link.Link} {link.Linkmode} {link.Address} {link.Broadcast}");
                }
            }
            else
            {
                output = "ps aux".Bash();
            }
            // Console.WriteLine(output);
        }
    }
}
