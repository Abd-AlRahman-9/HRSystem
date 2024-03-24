using System.Data;
using HRDomain.Entities;
using HRDomain.Specification.Params;
using Microsoft.Data.SqlClient;

namespace HRDomain.Repository
{
    public class ADOConnection
    {
        SqlConnection Connection;
        public ADOConnection (string ConString)
        {
            Connection = new SqlConnection (ConString);
        }
        public DataTable ExcuteMangersProcedure (string Procedure)
        {
            SqlCommand cmd = new SqlCommand(Procedure,Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Connection.Open ();
            SqlDataReader DR = cmd.ExecuteReader ();
            DataTable Managers = new DataTable ();
            Managers.Load(DR);
            Connection.Close();
            return Managers;
        }
        public Department ExcuteDepartmentProcedure (string Procedure,string name)
        {
            SqlCommand cmd = new SqlCommand (Procedure,Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter holderParameter = new SqlParameter
            {
                ParameterName = "@name",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = name,
            };
            Department Dept = new Department ();
            Connection.Open ();
            Dept = cmd.ExecuteScalar () as Department;
            Connection.Close ();
            return Dept;
        }
        public DataTable ExcuteSalariesProcedure (string Procedure,int StartMonth,int Year,int? EndMonth)
        {
            SqlCommand cmd = new SqlCommand(Procedure, Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter holderParameter = new SqlParameter
            {
                ParameterName = "@StartDate",
                SqlDbType = SqlDbType.Date,
                Direction = ParameterDirection.Input,
                Value = new DateTime(Year,StartMonth,1),
            };
            SqlParameter holderParameter1 = new SqlParameter
            {
                ParameterName = "@EndDate",
                SqlDbType = SqlDbType.Date,
                Direction = ParameterDirection.Input,
                Value = new DateTime(Year, EndMonth.HasValue ? EndMonth.Value : StartMonth,1),
            };
            cmd.Parameters.Add (holderParameter);
            cmd.Parameters.Add (holderParameter1);
            DataTable Salaries = new DataTable();
            Connection.Open();
            SqlDataReader DR = cmd.ExecuteReader();
            Salaries.Load(DR);
            Connection.Close();
            return Salaries;
        }
        public DataTable ExcuteSalProcedures (string Procedure,SalProcedureParams P)
        {
            SqlCommand cmd = new SqlCommand(Procedure, Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter holderParameter = new SqlParameter
            {
                ParameterName = "@NationalId",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                Value = P.NationalId.ToString(),
            };
            SqlParameter holderParameter0 = new SqlParameter
            {
                ParameterName = "@StartDate",
                SqlDbType = SqlDbType.Date,
                Direction = ParameterDirection.Input,
                Value = new DateTime(P.Year, P.StartMonth, 1),
            };
            SqlParameter holderParameter1 = new SqlParameter
            {
                ParameterName = "@EndDate",
                SqlDbType = SqlDbType.Date,
                Direction = ParameterDirection.Input,
                Value = new DateTime(P.Year, P.EndMonth.HasValue ? P.EndMonth.Value : P.StartMonth, 1),
            };
            cmd.Parameters.Add(holderParameter);
            cmd.Parameters.Add(holderParameter0);
            cmd.Parameters.Add(holderParameter1);
            DataTable Data = new DataTable();
            Connection.Open();
            SqlDataReader DR = cmd.ExecuteReader();
            Data.Load(DR);
            Connection.Close();
            return Data;
        }
    }
}
