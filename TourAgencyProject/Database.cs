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
        public Person getPerson(string serialPassport)
        {
            if (sqlConnection == null)
                throw new Exception("Нужно открыть подключение");
            string sqlQuery = $"select * from Person where passport_number = '{serialPassport}'";
            SqlCommand cmd = new SqlCommand( sqlQuery, sqlConnection);
            var reader = cmd.ExecuteReader();
            reader.Read();
            Person person;
            if (reader.HasRows)
            {
                person = new Person()
                {
                    Id = (int)reader["person_id"],
                    Lastname = reader["lastname"].ToString(),
                    Name = reader["name"].ToString(),
                    Patronymic = reader["patronymic"].ToString(),
                    Passport = reader["passport_number"].ToString(),
                    TourID = (int)reader["tour_id"],
                    FlyCount = (int)reader["fly_count"]
                };
            }
            else
                person = new Person()
                {
                    Name = "НЕПОЛЬЗОВАТЕЛЬ"
                };
            reader.Close();
            return person;
        }
        public Person getPerson(int ID)
        {
            if (sqlConnection == null)
                throw new Exception("Нужно открыть подключение");
            string sqlQuery = $"select * from Person where person_id = {ID}";
            SqlCommand cmd = new SqlCommand(sqlQuery, sqlConnection);
            var reader = cmd.ExecuteReader();
            reader.Read();
            Person person;
            if (reader.HasRows)
            {
                person = new Person()
                {
                    Id = (int)reader["person_id"],
                    Lastname = reader["lastname"].ToString(),
                    Name = reader["name"].ToString(),
                    Patronymic = reader["patronymic"].ToString(),
                    Passport = reader["passport_number"].ToString(),
                    TourID = (int)reader["tour_id"],
                    FlyCount = (int)reader["fly_count"]
                };
            }
            else
                person = new Person()
                {
                    Name = "НЕПОЛЬЗОВАТЕЛЬ"
                };
            reader.Close();
            return person;
        }
        public Tour getTour(int id)
        {
            if (sqlConnection == null)
                throw new Exception("Нужно открыть подключение");
            string sqlQuery = $"select * from Tour where tour_id = {id}";
            SqlCommand cmd = new SqlCommand(sqlQuery, sqlConnection);
            var reader = cmd.ExecuteReader();
            reader.Read();
                var tour = new Tour()
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
                };
            
            reader.Close();
            return tour;
        }
        public ObservableCollection<Records> getRecordList()
        {
            if (sqlConnection == null)
                throw new Exception("Нужно открыть подключение");
            string sqlQuery = "select * from Stats";
            SqlCommand cmd = new SqlCommand(sqlQuery, sqlConnection);
            ObservableCollection<Records> result = new ObservableCollection<Records>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Records()
                {
                    Name = reader["name"].ToString(),
                    OrderDate = DateTime.Parse(reader["order_date"].ToString()),
                    Person_id = (int)reader["person_id"]
                });
            }
            reader.Close();
            for (int i = 0; i < result.Count; i++)
            {
                Person person = getPerson(result[i].Person_id);
                result[i].PersonFIO = $"{person.Lastname} {person.Name} {person.Patronymic}";
            }
            return result;
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

        public void newRecord(Records record)
        {
            if (sqlConnection == null)
                throw new Exception("Нужно открыть подключение");
            string sqlQuery = $"insert into Stats values(N'{record.Name}', {record.Person_id}, {record.Tour_id}, '{record.OrderDate.ToString("yyyy-MM-dd HH:mm:ss.fff")}')";
            SqlCommand cmd = new SqlCommand(sqlQuery, sqlConnection);
            cmd.ExecuteNonQuery();
        }
    }
}
