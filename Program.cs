using System;

namespace CatWorx.BadgeMaker
{

   class Program
   {
      async static Task Main(string[] args)
      {

         List<Employee> employees = await PeopleFetcher.GetFromApi();
         Util.PrintEmployees(employees);
         Util.MakeCSV(employees);
         await Util.MakeBadges(employees);
      }
      //get employee names from the other method and display in console



   }
}
