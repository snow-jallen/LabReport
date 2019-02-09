using LabReport.Shared;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace LabReport.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hi there.");

            var imageVersionPath = @"c:\imageversion.txt";
            var imageVersionContent = "[ Not Found ]";
            if(File.Exists(imageVersionPath))
            {
                imageVersionContent = File.ReadAllText(imageVersionPath);
            }

            var host = "localhost";
            var port = 5000;
            var requestUri = $"http://{host}:{port}/api/ReportItems";
            var reportItem = new ReportItem()
            {
                HostName = Environment.MachineName,
                ImageVersionContent = imageVersionContent,
                ReportTime = DateTime.Now,
                SerialNumber = "???",
                MacNumber = "???"
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
    }
}
