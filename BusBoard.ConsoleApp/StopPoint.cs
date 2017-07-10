using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BusBoard.ConsoleApp
{
    class StopPoint
    {
        public double distance;
        public string commonName;
        public string id;

        private struct Temp
        {
            public List<StopPoints> stopPoints;
        }

        private struct StopPoints
        {
            public double distance;
            public string commonName;
            public string id;
        }

        public static List<StopPoint> FromJson(string input)
        {
            var converted = JsonConvert.DeserializeObject<Temp>(input);
            var stopPointList = new List<StopPoint>();
            foreach (var stop in converted.stopPoints)
            {
                var stopPoint = new StopPoint();
                stopPoint.commonName = stop.commonName;
                stopPoint.distance = stop.distance;
                stopPoint.id = stop.id;
                stopPointList.Add(stopPoint);
            }
            return stopPointList;
        }
    }
}
