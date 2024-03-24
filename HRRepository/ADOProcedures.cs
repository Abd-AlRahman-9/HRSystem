using System.Data;
using HRDomain.Entities;
using HRDomain.Entities.DrivenEntities;
using HRDomain.Repository;
using HRDomain.Specification.Params;

namespace HRRepository
{
    public class ADOProcedures
    {
        private readonly string _ConnectionString;
        public int SalariesCount;

        public ADOProcedures(string ConnectionString) 
        {
            _ConnectionString = ConnectionString;
        }
        // get managers
        public Dictionary<string,string> GetManagers ()
        {
            string _Procedure = "[dbo].[GetManagers]";
            ADOConnection getData  = new ADOConnection (_ConnectionString);
            Dictionary<string, string> Mangers = new Dictionary<string, string>();
            DataTable DT = getData.ExcuteMangersProcedure(_Procedure);
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                string Value = DT.Rows[i][0].ToString();
                string Key = DT.Rows[i][1].ToString();

                Mangers.Add(Key,Value.ToLower());
            }
            return Mangers;
        }
        // get Departement by name
        public Department GetDepartment(string name)
        {
            string _Procedure = "[dbo].[GetDepartment]";
            ADOConnection getData = new ADOConnection(_ConnectionString);
            Department Department = getData.ExcuteDepartmentProcedure(_Procedure,name);
            return Department;
        }
        // get salaries
        public List<SalaryObj> GetSalaries(SalariesParams P)
        {
            string _Procedure = "[dbo].[CalculateSalaries]";
            ADOConnection getData = new ADOConnection(_ConnectionString);
            List<SalaryObj> Salaries = new List<SalaryObj>();
            DataTable DT = getData.ExcuteSalariesProcedure(_Procedure, P.StartMonth,P.Year,P.EndMonth);
            SalariesCount = DT.Rows.Count;
            DT = DT.AsEnumerable()
                   .Where
                        (row => 
                            (
                            string.IsNullOrEmpty(P.Search) ||
                            row.Field<string>("EmployeeName").ToLower().Contains($"{P.Search}".ToLower()) || 
                            row.Field<string>("DepartmentName").ToLower().Contains($"{P.Search}".ToLower())
                            )
                        )
                    .Skip(P.PageSize * (P.PageIndex - 1))
                    .Take(P.PageSize)
                    .CopyToDataTable();
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                Salaries.Add
                (new SalaryObj()
                    {
                        EmployeeName = DT.Rows[i][0].ToString(),
                        NationalID = DT.Rows[i][1].ToString(),
                        DepartmentName = DT.Rows[i][2].ToString(),
                        BasicSalary = (decimal)DT.Rows[i][3],
                        AbsenceDays = (int)DT.Rows[i][4],
                        AttendDays = (int)DT.Rows[i][5],
                        OverallBonusHours = (decimal)DT.Rows[i][6],
                        OverallDiscountHours = (decimal)DT.Rows[i][7],
                        OverallDiscount = (decimal)DT.Rows[i][8],
                        OverallBonus = (decimal)DT.Rows[i][9],
                        NetSalary = (decimal)DT.Rows[i][10]
                    }
                );
            }
            return Salaries;
        }
        public Dictionary<DateOnly, string> GetAbsentDays(SalProcedureParams P)
        {
            string _Procedure = "[dbo].[GetAbsentDays]";
            ADOConnection getData = new ADOConnection(_ConnectionString);
            DataTable DT = getData.ExcuteSalProcedures(_Procedure, P);
            Dictionary<DateOnly, string> Data = new Dictionary<DateOnly, string>();
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                string Value = DT.Rows[i][0].ToString();
                DateOnly Key = DateOnly.Parse(DT.Rows[i][1].ToString());

                Data.Add(Key, Value.ToLower());
            }
            return Data;
        }
        public List<AttendObj> GetAttendToEmployee(SalProcedureParams P)
        {
            string _Procedure = "[dbo].[GetEmployeeAttendance]";
            ADOConnection getData = new ADOConnection(_ConnectionString);
            DataTable DT = getData.ExcuteSalProcedures(_Procedure, P);
            List<AttendObj> EmpAttend = new List<AttendObj>();
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                EmpAttend.Add
                (new AttendObj()
                    {
                        Attendance = TimeSpan.Parse(DT.Rows[i][0].ToString()),
                        Leave = TimeSpan.Parse(DT.Rows[i][1].ToString()),
                        Date = DateOnly.Parse(DT.Rows[i][2].ToString()),
                        Bonus = (decimal)DT.Rows[i][3],
                        Discount = (decimal)DT.Rows[i][4]
                    }
                );
            }
            return EmpAttend;
        }
        public List<AttendObj> GetLateDays(SalProcedureParams P) 
        {
            string _Procedure = "[dbo].[GetEmployeeLateDays]";
            ADOConnection getData = new ADOConnection(_ConnectionString);
            DataTable DT = getData.ExcuteSalProcedures(_Procedure, P);
            List<AttendObj> EmpAttend = new List<AttendObj>();
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                EmpAttend.Add
                (new AttendObj()
                {
                    Attendance = TimeSpan.Parse(DT.Rows[i][0].ToString()),
                    Leave = TimeSpan.Parse(DT.Rows[i][1].ToString()),
                    Date = DateOnly.Parse(DT.Rows[i][2].ToString()),
                    Bonus = (decimal)DT.Rows[i][3],
                    Discount = (decimal)DT.Rows[i][4]
                }
                );
            }
            return EmpAttend;
        }
    }
}
