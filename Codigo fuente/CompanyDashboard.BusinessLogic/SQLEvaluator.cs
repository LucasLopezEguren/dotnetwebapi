using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using CompanyDashboard.BusinessLogic.Interface;
using CompanyDashboard.Domain;
using CompanyDashboard.DataAccess.Interface;
using CompanyDashboard.DataAccess;
using CompanyDashboard.BusinessLogic.Exceptions;

namespace CompanyDashboard.BusinessLogic
{
    public class SQLEvaluator : IEvaluator
    {
        public virtual String[] Evaluate(Node factor)
        {
            AreaLogic al = new AreaLogic(null);
            String[] result = new String[2];

            String connection = al.GetAreaByID(factor.Area).DataSource;
            String query = factor.Text;
            SqlConnection dbConnection = new SqlConnection(connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable ds = new DataTable();

            dbConnection.Open();
            SqlCommand sql = new SqlCommand();
            sql.CommandText = query;
            sql.CommandType = CommandType.Text;
            sql.Connection = dbConnection;

            try
            {
                String queryValue = (String)sql.ExecuteScalar();
                result[1] = queryValue;
                result[0] = "1";
            }
            catch
            {
                Int32 queryValue = (Int32)sql.ExecuteScalar();
                result[1] = queryValue.ToString();
                result[0] = "2";
            }
            adapter.SelectCommand = sql;
            adapter.Fill(ds);
            dbConnection.Close();

            return result;
        }
    }
}
