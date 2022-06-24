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
                    ID = (int)reader["tour_id"],
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
            reader.Close();
            return result;
        }

        public bool clientAlready(string serialPassport)
        {
            if (sqlConnection == null)
                throw new Exception("Нужно открыть подключение");
            string sqlQuery = $"select * from Person where passport_number = '{serialPassport}'";
            SqlCommand cmd = new SqlCommand(sqlQuery, sqlConnection);
            var reader = cmd.ExecuteReader();
            bool result = reader.HasRows;
            reader.Close();
            return result;
        }
        public int GetPersonFlyCount(string serialPassport)
        {
            if (sqlConnection == null)
                throw new Exception("Нужно открыть подключение");
            string sqlQuery = $"select * from Person where passport_number = '{serialPassport}'";
            SqlCommand cmd = new SqlCommand(sqlQuery, sqlConnection);
            var reader = cmd.ExecuteReader();
            reader.Read();
            int count = (int)reader["fly_count"];
            reader.Close();
            return count;
        }
        public void newClient(Person client)
        {
            if (sqlConnection == null)
                throw new Exception("Нужно открыть подключение");
            string sqlQuery = $"insert into Person values" +
                $"(N'{client.Lastname}', N'{client.Name}', N'{client.Patronymic}', N'{client.Passport}', {client.TourID}, {client.FlyCount})";
            SqlCommand cmd = new SqlCommand(sqlQuery, sqlConnection);
            cmd.ExecuteNonQuery();
        }
        public void updatePerson(Person client)
        {
            if (sqlConnection == null)
                throw new Exception("Нужно открыть подключение");
            string sqlQuery = $"update Person set fly_count = {client.FlyCount}, tour_id = {client.TourID} where passport_number = '{client.Passport}'";
            SqlCommand cmd = new SqlCommand(sqlQuery, sqlConnection);
            cmd.ExecuteNonQuery();
        }
    }
}
