using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace BusBoard.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = GetInput();
            var downloadedJson = DownloadFile(input);
            var busList = Bus.FromJson(downloadedJson);
            var firstFiveBus = busList.OrderBy(x => x.expectedArrival).Take(5);
            foreach (var bus in firstFiveBus)
            {
                bus.Print();
            }
        }

        private static string GetInput()
        {
            return Console.ReadLine();
        }

        private static string DownloadFile(string input)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var address = @"https://api.tfl.gov.uk/StopPoint/" + input +
                @"/Arrivals?app_id=2960d12f&app_key=f75abe891c0d1aec03fd1e396b9afe40";
            var client = new RestClient();
            client.BaseUrl = new Uri(@"https://api.tfl.gov.uk");
            client.Authenticator = new SimpleAuthenticator("app_id", "2960d12f", "app_key", "f75abe891c0d1aec03fd1e396b9afe40");

            var request = new RestRequest();
            request.Resource = @"StopPoint/" + input + @"/Arrivals";


            var response = client.Execute(request);
            return response.Content;

        }
    }
}
