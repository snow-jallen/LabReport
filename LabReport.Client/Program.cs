using LabReport.Shared;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.IO;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Management;
using System.Collections.Generic;

namespace LabReport.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hi there.");

            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            var host = config["host"] ?? "smartdeploy.ad.snow.edu";
            var port = config["port"] ?? "5000";

            var imageVersionPath = @"c:\imageversion.txt";
            var imageVersionContent = "[ Not Found ]";
            if(File.Exists(imageVersionPath))
            {
                imageVersionContent = File.ReadAllText(imageVersionPath);
            }

            var requestUri = $"http://{host}:{port}/api/ReportItems";
            var reportItem = new ReportItem()
            {
                HostName = Environment.MachineName,
                ImageVersionContent = imageVersionContent,
                ReportTime = DateTime.Now,
                SerialNumber = getSerialNumber(),
                MacNumber = getMacAddress()
            };
            var stringifiedValue = JsonConvert.SerializeObject(reportItem);
            
            using (var client = new HttpClient())
            {
                try
                {
                    Console.WriteLine($"Posting to {requestUri}...");
                    var content = new StringContent(stringifiedValue, System.Text.Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(requestUri, content);

                    Console.WriteLine(response);
                    Console.WriteLine("All done. :)");
                }
                catch(Exception ex)
                {
                    Console.Write($"Whoops!\n{ex}");
                }
            }
        }

        private static string getSerialNumber()
        {
            var serialNumber = "???";
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT Product, SerialNumber FROM Win32_BaseBoard"))
                {
                    var information = searcher.Get();
                    var values = new List<string>();
                    foreach (var obj in information)
                    {
                        foreach (var data in obj.Properties)
                        {
                            if(data.Name.IndexOf("Serial", StringComparison.CurrentCultureIgnoreCase) >= 0)
                                values.Add(data.Value.ToString());
                        }
                    }
                    serialNumber = String.Join("<br/>", values);
                }
            }
            catch { }
            return serialNumber;
        }

        private static string getMacAddress()
        {
            var mac = "???";

            try
            {
                mac = String.Join("\n", NetworkInterface.GetAllNetworkInterfaces()
                                                        .Where(i=>i.Name == "Ethernet")
                                                        .Select(i => i.GetPhysicalAddress().ToString()));
            }
            catch { }

            return mac;
        }
    }
}
