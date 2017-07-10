using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusBoard.ConsoleApp;

namespace BusBoard.Api
{
    public class StopAndBus
    {
        public string StopName { get; }
        public List<Bus> NextFive { get; }

        public StopAndBus(string name, List<Bus> bus)
        {
            StopName = name;
            NextFive = bus;
        }
        
    }
}
