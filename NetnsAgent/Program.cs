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
            try
            {
                Console.WriteLine("[LOG] Starting NetnsAgent application");
                
                // Check if we have any command line arguments for the namespace
                string networkNamespace = args.Length > 0 ? args[0] : "red";
                Console.WriteLine($"[LOG] Using network namespace: {networkNamespace}");

                string command;
                if (networkNamespace.Equals("default", StringComparison.OrdinalIgnoreCase))
                {
                    command = "ip -j addr";
                    Console.WriteLine("[LOG] Using default network namespace (no -n flag)");
                }
                else
                {
                    command = $"ip -j -n {networkNamespace} addr";
                }
                
                Console.WriteLine($"[LOG] Preparing to execute command: {command}");

                var output = command.Bash();

                if (string.IsNullOrWhiteSpace(output))
                {
                    Console.WriteLine("[WARNING] Command returned empty output");
                    return;
                }

                Console.WriteLine($"[LOG] Command output received, attempting to parse JSON");
                
                List<IPLink> links;
                try
                {
                    links = JsonConvert.DeserializeObject<List<IPLink>>(output);
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"[ERROR] Failed to parse JSON output: {ex.Message}");
                    Console.WriteLine($"[ERROR] Raw output was: {output}");
                    throw;
                }

                if (links == null || links.Count == 0)
                {
                    Console.WriteLine("[WARNING] No network links found or parsed from output");
                    return;
                }

                Console.WriteLine($"[LOG] Successfully parsed {links.Count} network link(s)");

                foreach (var link in links)
                {
                    try
                    {
                        Console.WriteLine($"# {link.Ifname ?? "N/A"} {link.Ifindex} {link.Link} {link.Linkmode ?? "N/A"} {link.Address ?? "N/A"} {link.Broadcast ?? "N/A"}");

                        if (link.AddrInfo != null && link.AddrInfo.Count > 0)
                        {
                            Console.WriteLine($"[LOG] Processing {link.AddrInfo.Count} address info item(s) for interface {link.Ifname}");
                            
                            foreach (var addr in link.AddrInfo)
                            {
                                if (addr != null)
                                {
                                    Console.WriteLine($"{addr.Label ?? "N/A"} = {addr.Local ?? "N/A"}");
                                }
                                else
                                {
                                    Console.WriteLine("[WARNING] Found null address info item");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"[LOG] No address info found for interface {link.Ifname}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] Error processing link {link?.Ifname ?? "unknown"}: {ex.Message}");
                        // Continue processing other links
                    }
                }

                Console.WriteLine("[LOG] NetnsAgent application completed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Fatal error in NetnsAgent application: {ex.Message}");
                Console.WriteLine($"[ERROR] Stack trace: {ex.StackTrace}");
                Environment.Exit(1);
            }
        }
    }
}
