using System;
using System.Collections.Generic;
using MangementPermission.Service.Service;

namespace ManagementPermission
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new List<string>();

            //Read input
            Console.WriteLine("Please insert input (input exit for end of file):");
            while (true)
            {
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line) || line.Equals("Exit"))
                {
                    break;
                }
                input.Add(line);
            }

            //TODO: call to service executed process
            var structureService = new StructureService();
            var test = structureService.CreateCompanyStructure(input);
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
