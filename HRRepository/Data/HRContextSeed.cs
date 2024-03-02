//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;
//using HRDomain.Entities;
//using Microsoft.Extensions.Logging;

//namespace HRRepository.Data
//{
//    public class HRContextSeed
//    {
//        public static async Task SeedAsync(HRContext context,ILoggerFactory loggerFactory)
//        {
//            try
//            {
//                //NOTE:you must care about the order of seeding
//                // do all the code inside the if statement to each entity
//                if (!context.Employees.Any())
//                {
//                    var EmployeesData = File.ReadAllText("../HRRepository/Data/DataSeeding/Employees.json");
//                    var Employees = JsonSerializer.Deserialize<List<Employee>>(EmployeesData);
//                    foreach (var Employee in Employees)
//                    {
//                        context.Set<Employee>().Add(Employee);
//                    }
//                }
//                await context.SaveChangesAsync();
//            }
//            catch(Exception ex)
//            {
//                var logger = loggerFactory.CreateLogger<HRContextSeed>();
//                logger.LogError(ex,ex.Message);
//            }

//        }
//    }
//}
