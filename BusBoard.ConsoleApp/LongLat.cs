using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BusBoard.ConsoleApp
{
    class LongLat
    {
        private struct Result
        {
            public double longitude;
            public double latitude;

        }
        private struct Temp
        {
            public Result result;
        }
        public double longitude;
        public double latitude;

        public static LongLat FromJson(string input)
        {
            var converted = JsonConvert.DeserializeObject<Temp>(input);
            var longlat = new LongLat();
            longlat.longitude = converted.result.longitude;
            longlat.latitude = converted.result.latitude;
            return longlat;
        }
    }
}
