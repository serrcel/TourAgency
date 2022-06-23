using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

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

        public ObservableCollection<Tour> getTourList()
        {
            if (sqlConnection == null)
                throw new Exception("Нужно открыть подключение") ;
            string sqlQuery = "select * from Tour";
            SqlCommand cmd = new SqlCommand(sqlQuery, sqlConnection);
            ObservableCollection<Tour> result = new ObservableCollection<Tour>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Tour()
                {
                    Name = reader["name"].ToString(),
                    Description = reader["description"].ToString(),
                    Arrival = reader["arrival"].ToString(),
                    Departure = reader["departure"].ToString(),
                    Cost = (int)reader["cost"],
                    Programm = (int)reader["tour_programm"],
                    Date = DateTime.Parse(reader["date"].ToString()),
                    DateBack = DateTime.Parse(reader["date_back"].ToString())
                });
            }
            return result;
        }
    }
}
