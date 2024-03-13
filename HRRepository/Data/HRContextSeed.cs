////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Text;
////using System.Text.Json;
////using System.Threading.Tasks;
////using HRDomain.Entities;
////using Microsoft.Extensions.Logging;

//<<<<<<< HEAD
//namespace HRRepository.Data
//{
//    public class HRContextSeed
//    {
//        public static async Task SeedAsync(HRContext context, ILoggerFactory loggerFactory)
//        {
//            try
//            {
//                //if (!context.Vacations.Any())
//                //{
//                //    var vacationsData = File.ReadAllText("../HRRepository/Data/DataSeeding/Vacation.json");
//                //    var Vacations = JsonSerializer.Deserialize<List<Vacation>>(vacationsData);
//                //    foreach (var vacation in Vacations)
//                //    {
//                //        context.Set<Vacation>().Add(vacation);
//                //    }
//=======
////namespace HRRepository.Data
////{
////    public class HRContextSeed
////    {
////        public static async Task SeedAsync(HRContext context, ILoggerFactory loggerFactory)
////        {
////            try
////            {
////                //if (!context.Vacations.Any())
////                //{
////                //    var vacationsData = File.ReadAllText("../HRRepository/Data/DataSeeding/Vacation.json");
////                //    var Vacations = JsonSerializer.Deserialize<List<Vacation>>(vacationsData);
////                //    foreach (var vacation in Vacations)
////                //    {
////                //        context.Set<Vacation>().Add(vacation);
////                //    }
//>>>>>>> 80d51a61c6ef12d91368047e62613b859414f3d3

////                //}

////                //if (!context.Departments.Any())
////                //{
////                //    var departmentsData = File.ReadAllText("../HRRepository/Data/DataSeeding/Department.json");
////                //    var Departments = JsonSerializer.Deserialize<List<Department>>(departmentsData);
////                //    foreach (var department in Departments)
////                //    {
////                //        context.Set<Department>().Add(department);
////                //    }
////                //}
////                //NOTE:you must care about the order of seeding
////                // do all the code inside the if statement to each entity
////                //if (!context.Employees.Any())
////                //{
////                //    var EmployeesData = File.ReadAllText("../HRRepository/Data/DataSeeding/Employee.json");
////                //    var Employees = JsonSerializer.Deserialize<List<Employee>>(EmployeesData);

////                //    foreach (var Employee in Employees)
////                //    {
////                //        context.Set<Employee>().Add(Employee);
////                //    }

                    
//<<<<<<< HEAD
//                    await context.SaveChangesAsync();
//                }
//                //if (!context.EmployeeAttendaces.Any())
//                //{
//                //    var attendData = File.ReadAllText("../HRRepository/Data/DataSeeding/EmployeeAttendace.json");
//                //    var attendances = JsonSerializer.Deserialize<List<EmployeeAttendace>>(attendData);
//                //    foreach (var attendance in attendances)
//                //    {
//                //        // Retrieve the corresponding employee from the context
//                //        var employee = context.Employees.FirstOrDefault(e => e.Id == attendance.EmployeeId);
//                //        if (employee != null)
//                //        {
//                //            attendance.Employee = employee; // Set the navigation property
//                //            context.Set<EmployeeAttendace>().Add(attendance);
//                //        }
//                //    }
//                //}
//                //if (!context.EmployeeVacations.Any())
//                //{
//                //    var vacationData = File.ReadAllText("../HRRepository/Data/DataSeeding/EmployeeVacation.json");
//                //    var vacations = JsonSerializer.Deserialize<List<EmployeeVacation>>(vacationData);
//                //    foreach (var vacation in vacations)
//                //    {
//                //        // Retrieve the corresponding employee from the context
//                //        var employee = context.Employees.FirstOrDefault(e => e.Id == vacation.EmployeeId);
//                //        if (employee != null)
//                //        {
//                //            vacation.Employee = employee; // Set the navigation property
//                //            context.Set<EmployeeVacation>().Add(vacation);
//                //        }
//                //    }
//                //}
//                if (!context.Vacations.Any())
//                {
//                    var vacationsData = File.ReadAllText("../HRRepository/Data/DataSeeding/Vacation.json");
//                    var Vacations = JsonSerializer.Deserialize<List<Vacation>>(vacationsData);
//                    foreach (var vacation in Vacations)
//                    {
//                        context.Set<Vacation>().Add(vacation);
//                    }
//                }
//                    if (!context.Departments.Any())
//                    {
//                        var departmentsData = File.ReadAllText("../HRRepository/Data/DataSeeding/Department.json");
//                        var Departments = JsonSerializer.Deserialize<List<Department>>(departmentsData);
//                        foreach (var department in Departments)
//                        {
//                            context.Set<Department>().Add(department);
//                        }
//                    }
//                    //NOTE:you must care about the order of seeding
//                    // do all the code inside the if statement to each entity
//                    if (!context.Employees.Any())
//                    {
//                        var EmployeesData = File.ReadAllText("../HRRepository/Data/DataSeeding/Employee.json");
//                        var Employees = JsonSerializer.Deserialize<List<Employee>>(EmployeesData);
//=======
////                //    await context.SaveChangesAsync();
////                //}
////                //if (!context.EmployeeAttendaces.Any())
////                //{
////                //    var attendData = File.ReadAllText("../HRRepository/Data/DataSeeding/EmployeeAttendace.json");
////                //    var attendances = JsonSerializer.Deserialize<List<EmployeeAttendace>>(attendData);
////                //    foreach (var attendance in attendances)
////                //    {
////                //        // Retrieve the corresponding employee from the context
////                //        var employee = context.Employees.FirstOrDefault(e => e.Id == attendance.EmployeeId);
////                //        if (employee != null)
////                //        {
////                //            attendance.Employee = employee; // Set the navigation property
////                //            context.Set<EmployeeAttendace>().Add(attendance);
////                //        }
////                //    }
////                //}
////                //if (!context.EmployeeVacations.Any())
////                //{
////                //    var vacationData = File.ReadAllText("../HRRepository/Data/DataSeeding/EmployeeVacation.json");
////                //    var vacations = JsonSerializer.Deserialize<List<EmployeeVacation>>(vacationData);
////                //    foreach (var vacation in vacations)
////                //    {
////                //        // Retrieve the corresponding employee from the context
////                //        var employee = context.Employees.FirstOrDefault(e => e.Id == vacation.EmployeeId);
////                //        if (employee != null)
////                //        {
////                //            vacation.Employee = employee; // Set the navigation property
////                //            context.Set<EmployeeVacation>().Add(vacation);
////                //        }
////                //    }
////                //}
////                if (!context.Vacations.Any())
////                {
////                    var vacationsData = File.ReadAllText("../HRRepository/Data/DataSeeding/Vacation.json");
////                    var Vacations = JsonSerializer.Deserialize<List<Vacation>>(vacationsData);
////                    foreach (var vacation in Vacations)
////                    {
////                        context.Set<Vacation>().Add(vacation);
////                    }
////                }
////                    if (!context.Departments.Any())
////                    {
////                        var departmentsData = File.ReadAllText("../HRRepository/Data/DataSeeding/Department.json");
////                        var Departments = JsonSerializer.Deserialize<List<Department>>(departmentsData);
////                        foreach (var department in Departments)
////                        {
////                            context.Set<Department>().Add(department);
////                        }
////                    }
////                    //NOTE:you must care about the order of seeding
////                    // do all the code inside the if statement to each entity
////                    if (!context.Employees.Any())
////                    {
////                        var EmployeesData = File.ReadAllText("../HRRepository/Data/DataSeeding/Employee.json");
////                        var Employees = JsonSerializer.Deserialize<List<Employee>>(EmployeesData);
//>>>>>>> 80d51a61c6ef12d91368047e62613b859414f3d3

////                        foreach (var Employee in Employees)
////                        {
////                            context.Set<Employee>().Add(Employee);
////                        }
////                    }

//<<<<<<< HEAD
//                    if (!context.EmployeeAttendaces.Any())
//                    {
//                        var attendData = File.ReadAllText("../HRRepository/Data/DataSeeding/EmployeeAttendace.json");
//                        var attendances = JsonSerializer.Deserialize<List<EmployeeAttendace>>(attendData);
//                        foreach (var attendance in attendances)
//                        {
//                            // Retrieve the corresponding employee from the context
//                            var employee = context.Employees.FirstOrDefault(e => e.Id == attendance.EmployeeId);
//                            if (employee != null)
//                            {
//                                attendance.Employee = employee; // Set the navigation property
//                                context.Set<EmployeeAttendace>().Add(attendance);
//                            }
//                        }
//                    }
//                    if (!context.EmployeeVacations.Any())
//                    {
//                        var vacationData = File.ReadAllText("../HRRepository/Data/DataSeeding/EmployeeVacation.json");
//                        var vacations = JsonSerializer.Deserialize<List<EmployeeVacation>>(vacationData);
//                        foreach (var vacation in vacations)
//                        {
//                            // Retrieve the corresponding employee from the context
//                            var employee = context.Employees.FirstOrDefault(e => e.Id == vacation.EmployeeId);
//                            if (employee != null)
//                            {
//                                vacation.Employee = employee; // Set the navigation property
//                                context.Set<EmployeeVacation>().Add(vacation);
//                            }
//                        }   
//                    }
//                await context.SaveChangesAsync();
//            }
//            catch (Exception ex)
//            {
//                var logger = loggerFactory.CreateLogger<HRContextSeed>();
//                logger.LogError(ex, ex.Message);
//            }

//        }
//    }
//}



//=======
////                    if (!context.EmployeeAttendaces.Any())
////                    {
////                        var attendData = File.ReadAllText("../HRRepository/Data/DataSeeding/EmployeeAttendace.json");
////                        var attendances = JsonSerializer.Deserialize<List<EmployeeAttendace>>(attendData);
////                        foreach (var attendance in attendances)
////                        {
////                            // Retrieve the corresponding employee from the context
////                            var employee = context.Employees.FirstOrDefault(e => e.Id == attendance.EmployeeId);
////                            if (employee != null)
////                            {
////                                attendance.Employee = employee; // Set the navigation property
////                                context.Set<EmployeeAttendace>().Add(attendance);
////                            }
////                        }
////                    }
////                    if (!context.EmployeeVacations.Any())
////                    {
////                        var vacationData = File.ReadAllText("../HRRepository/Data/DataSeeding/EmployeeVacation.json");
////                        var vacations = JsonSerializer.Deserialize<List<EmployeeVacation>>(vacationData);
////                        foreach (var vacation in vacations)
////                        {
////                            // Retrieve the corresponding employee from the context
////                            var employee = context.Employees.FirstOrDefault(e => e.Id == vacation.EmployeeId);
////                            if (employee != null)
////                            {
////                                vacation.Employee = employee; // Set the navigation property
////                                context.Set<EmployeeVacation>().Add(vacation);
////                            }
////                        }   
////                    }
////                await context.SaveChangesAsync();
////            }
////            catch (Exception ex)
////            {
////                var logger = loggerFactory.CreateLogger<HRContextSeed>();
////                logger.LogError(ex, ex.Message);
////            }

////        }
////    }
////}


//>>>>>>> 80d51a61c6ef12d91368047e62613b859414f3d3
