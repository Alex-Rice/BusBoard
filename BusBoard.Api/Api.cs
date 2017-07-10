using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using BusBoard.ConsoleApp;
using RestSharp;
using RestSharp.Authenticators;

namespace BusBoard.Api
{
    public class ApiClass
    {
        public static ApiReturn MakeApiCalls(string input)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var postCodeInfo = DownloadPostcode(input);
                var longLat = LongLat.FromJson(postCodeInfo);
                var busStopJson = DownloadBusStops(longLat);
                var stopPoints = StopPoint.FromJson(busStopJson);
                var firstTwoStopPoints = stopPoints.OrderBy(x => x.distance).Take(2).ToList();
                return new ApiReturn(OutputAllBus(firstTwoStopPoints));
            }
            catch (InvalidPostcodeException)
            {
                return new ApiReturn();
            }
            catch (Exception e)
            {
                return new ApiReturn(e);
            }

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
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new InvalidPostcodeException();
            }
            return response.Content;

        }

        private static string DownloadBusStops(LongLat input)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(@"https://api.tfl.gov.uk");
            client.Authenticator = new SimpleAuthenticator("app_id", "2960d12f", "app_key", "f75abe891c0d1aec03fd1e396b9afe40");

            var request = new RestRequest();
            request.Resource = $"StopPoint?stopTypes=NaptanPublicBusCoachTram&radius=1000&lat={input.latitude}&lon={input.longitude}";


            var response = client.Execute(request);
            return response.Content;

        }

        private static List<StopAndBus> OutputAllBus(List<StopPoint> stops)
        {
            return stops.Select(OutputBus).ToList();
        }

        private static StopAndBus OutputBus(StopPoint stop)
        {
            var downloadedJson = DownloadFile(stop.id);
            var busList = Bus.FromJson(downloadedJson);
            var firstFiveBus = busList.OrderBy(x => x.expectedArrival).Take(5);
            return new StopAndBus(stop.commonName,firstFiveBus.ToList());
        }
    }
}
