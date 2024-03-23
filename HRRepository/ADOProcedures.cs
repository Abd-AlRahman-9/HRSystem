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
        public DataTable GetSalaries (int _StartMonth,int _Year,int? _EndMonth)
        {
            string _Procedure = "[dbo].[CalculateEmployeeSalary]";
            ADOConnection getData = new ADOConnection(_ConnectionString);
            DataTable Salaries = getData.ExcuteSalariesProcedure(_Procedure, _StartMonth,_Year,_EndMonth);
            return Salaries;
        }
    }
}
