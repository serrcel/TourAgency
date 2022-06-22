using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace TourAgencyProject
{
    public class Database
    {
        private SqlConnection sqlConnection;
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public void Open()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }

        public void Close()
        {
            if (sqlConnection != null)
                sqlConnection.Close();
            else
                return;
        }
    }
}
