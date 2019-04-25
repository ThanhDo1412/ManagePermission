using System;
using System.Collections.Generic;

namespace ManagementPermission
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new List<string>();

            //Read input
            Console.WriteLine("Please insert input (finish by escape button):");
            while (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                var line = Console.ReadLine();
                input.Add(line);
            }

            //TODO: call to service executed process
            var output = new List<string>();

            //Display output
            Console.WriteLine("Output as below:");
            foreach (var line in output)
            {
                Console.WriteLine(line);
            }
        }
    }
}
