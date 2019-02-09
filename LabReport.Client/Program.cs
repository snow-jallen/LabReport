using LabReport.Shared;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;

namespace LabReport.Client
{
    class Program
    {
        static void Main(string[] args)
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
            var requestUri = $"http://{host}:{port}";
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
                    client.PostAsync(requestUri, new StringContent(stringifiedValue));
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
