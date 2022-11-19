using Newtonsoft.Json.Linq;
namespace CatWorx.BadgeMaker
{
   class PeopleFetcher
   {
      //get employee names via command line input return the array opf employee names
      public static List<Employee> GetEmployees()
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
      async public static Task<List<Employee>> GetFromApi()
      {
         List<Employee> employees = new List<Employee>();
         using (HttpClient client = new HttpClient())
         {
            string response = await client.GetStringAsync("https://randomuser.me/api/?results=10&nat=us&inc=name,id,picture");
            JObject person = JObject.Parse(response);
            foreach (JToken token in person.SelectToken("results")!)
            {
               // Parse JSON data
               Employee emp = new Employee
               (
                 token.SelectToken("name.first")!.ToString(),
                 token.SelectToken("name.last")!.ToString(),
                 Int32.Parse(token.SelectToken("id.value")!.ToString().Replace("-", "")),
                 token.SelectToken("picture.large")!.ToString()
               );
               employees.Add(emp);
            }
         }

         return employees;
      }
   }

}