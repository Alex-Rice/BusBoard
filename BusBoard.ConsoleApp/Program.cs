using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
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
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var input = GetInput();
            var postCodeInfo = DownloadPostcode(input);
            var longLat = LongLat.FromJson(postCodeInfo);
            var busStopJson = DownloadBusStops(longLat);
            var stopPoints = StopPoint.FromJson(busStopJson);
            var firstTwoStopPoints = stopPoints.OrderBy(x => x.distance).Take(2).ToList();
            PrintAllBus(firstTwoStopPoints);
        }

        private static string GetInput()
        {
            Console.Write("Enter Postcode: ");
            return Console.ReadLine();
        }

        private static string DownloadFile(string input)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(@"https://api.tfl.gov.uk");
            client.Authenticator = new SimpleAuthenticator("app_id", "2960d12f", "app_key", "f75abe891c0d1aec03fd1e396b9afe40");

            var request = new RestRequest();
            request.Resource = @"StopPoint/" + input + @"/Arrivals";


            var response = client.Execute(request);
            return response.Content;

        }
        private static string DownloadPostcode(string input)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(@"https://api.postcodes.io");

            var request = new RestRequest();
            request.Resource = @"postcodes/" + input;


            var response = client.Execute(request);
            return response.Content;

        }

        private static string DownloadBusStops(LongLat input)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(@"https://api.tfl.gov.uk");
            client.Authenticator = new SimpleAuthenticator("app_id", "2960d12f", "app_key", "f75abe891c0d1aec03fd1e396b9afe40");

            var request = new RestRequest();
            request.Resource = @"StopPoint?stopTypes=NaptanPublicBusCoachTram&radius=1000&lat=51.5539350896633&lon=-0.14475403941841";


            var response = client.Execute(request);
            return response.Content;

        }

        private static void PrintAllBus(List<StopPoint> stops)
        {
            foreach (var stopPoint in stops)
            {
                PrintBus(stopPoint);
            }
        }

        private static void PrintBus(StopPoint stop)
        {
            Console.WriteLine(stop.commonName);
            var downloadedJson = DownloadFile(stop.id);
            var busList = Bus.FromJson(downloadedJson);
            var firstFiveBus = busList.OrderBy(x => x.expectedArrival).Take(5);
            foreach (var bus in firstFiveBus)
            {
                bus.Print();
            }
        }
    }
}

