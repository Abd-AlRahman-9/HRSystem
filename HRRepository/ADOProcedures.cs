using System.Data;
using HRDomain.Entities;
using HRDomain.Repository;

namespace HRRepository
{
    public class ADOProcedures
    {
        private readonly string _ConnectionString;

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
        public List<SalaryObj> GetSalaries (int _StartMonth,int _Year,int? _EndMonth)
        {
            string _Procedure = "[dbo].[CalculateEmployeeSalary]";
            ADOConnection getData = new ADOConnection(_ConnectionString);
            List<SalaryObj> Salaries = new List<SalaryObj>();
            DataTable DT = getData.ExcuteSalariesProcedure(_Procedure, _StartMonth,_Year,_EndMonth);
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                Salaries.Add
                (new SalaryObj()
                    {
                        EmployeeName = DT.Rows[i][0].ToString(),
                        DepartmentName = DT.Rows[i][1].ToString(),
                        BasicSalary = (decimal)DT.Rows[i][2],
                        AbsenceDays = (int)DT.Rows[i][3],
                        AttendDays = (int)DT.Rows[i][4],
                        OverallBonusHours = (decimal)DT.Rows[i][5],
                        OverallDiscountHours = (decimal)DT.Rows[i][6],
                        OverallDiscount = (decimal)DT.Rows[i][7],
                        OverallBonus = (decimal)DT.Rows[i][8],
                        NetSalary = (decimal)DT.Rows[i][DT.Columns.Count-1]
                    }
                );
            }
            return Salaries;
        }
    }
}
