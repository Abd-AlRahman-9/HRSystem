using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRDomain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;

namespace HRDomain.Repository
{
    public class ADOConnection
    {
        SqlConnection Connection;
        DataTable Managers;
        public ADOConnection (string ConString)
        {
            Connection = new SqlConnection (ConString);
            Managers = new DataTable ();
        }
        public DataTable ExcuteMangersProcedure (string Procedure)
        {
            SqlCommand cmd = new SqlCommand(Procedure,Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Connection.Open ();
            SqlDataReader DR = cmd.ExecuteReader ();
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
        public void ExcuteSalariesProcedure (string Procedure)
        {
            SqlCommand cmd = new SqlCommand(Procedure,Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        }
    }
}
