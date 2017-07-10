using System;
using System.Collections.Generic;
using BusBoard.Api;


namespace BusBoard.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = GetInput();
            var output = ApiClass.MakeApiCalls(input);
            PrintOutput(output);
        }

        private static string GetInput()
        {
            Console.Write("Enter Postcode: ");
            return Console.ReadLine();
        }

        private static void PrintOutput(List<StopAndBus> toPrint)
        {
            foreach (var stop in toPrint)
            {
                PrintStop(stop);
            }
        }

        private static void PrintStop(StopAndBus stop)
        {
            Console.WriteLine(stop.StopName);
            foreach (var bus in stop.NextFive)
            {
                Console.WriteLine(bus.Print());
            }
            Console.WriteLine();
        }
    }
}

