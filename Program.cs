using System;

namespace CatWorx.BadgeMaker
{

   class Program
   {
      async static Task Main(string[] args)
      {

         List<Employee> employees = GetEmployees();
         Util.PrintEmployees(employees);
         Util.MakeCSV(employees);
         await Util.MakeBadges(employees);
      }
      //get employee names from the other method and display in console

      //get employee names via command line input return the array opf employee names
      static List<Employee> GetEmployees()
      {
         List<Employee> employees = new List<Employee>();
         while (true)
         {
            Console.WriteLine("Please enter first name: (leave empty to exit): ");
            string firstName = Console.ReadLine() ?? "";
            if (firstName == "")
            {
               break;
            }
            Console.WriteLine("Please enter last name: ");
            string lastName = Console.ReadLine() ?? "";
            Console.WriteLine("Please enter an id: ");
            int id = Int32.Parse(Console.ReadLine() ?? "");
            Console.WriteLine("Please enter an photo url: ");
            string photoUrl = Console.ReadLine() ?? "";
            Employee currentEmployee = new Employee(firstName, lastName, id, photoUrl);

            employees.Add(currentEmployee);
         }
         return employees;
      }

   }
}
