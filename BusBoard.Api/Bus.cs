using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BusBoard.ConsoleApp
{
    class Bus
    {
        public string lineName;
        public string destinationName;
        public DateTime expectedArrival;

        public string Print()
        {
            return $"{lineName}\t{destinationName}\t{(expectedArrival - DateTime.UtcNow).Minutes}";
        }
        public static List<Bus> FromJson(string inputJson)
        {
            return JsonConvert.DeserializeObject<List<Bus>>(inputJson);
        }
    }
}
