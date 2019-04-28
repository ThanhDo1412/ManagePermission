using MangementPermission.Service.Service;
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

            //Create array of users and get all permission
            var structureService = new StructureService();
            var company = structureService.CreateCompanyStruture(input);
            var output = structureService.GetPermissionsOfCompany(company);

            //Display output
            Console.WriteLine("Output as below:");
            foreach (var line in output)
            {
                Console.WriteLine(line);
            }

            Console.ReadLine();
        }
    }
}
