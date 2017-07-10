using System;
using BusBoard.Api;


namespace BusBoard.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = GetInput();
            Console.WriteLine(ApiClass.MakeApiCalls(input));
        }

        private static string GetInput()
        {
            Console.Write("Enter Postcode: ");
            return Console.ReadLine();
        }
    }
}

