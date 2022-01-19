using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Data.Sqlite;

namespace TimeTable
{
    public class User
    {
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public static IEnumerable<User> GetAllDataFromTable()
        {
            List<User> users = new();
            string expression = @"SELECT Login as login, Password as pass, Status as status
                                FROM Users";

            using (SqliteConnection sqliteConnection = new("Data Source=Data/TimeTableDB.db;Mode=ReadOnly"))
            {
                sqliteConnection.Open();
                SqliteCommand command = new(expression, sqliteConnection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            users.Add(new User() { Login = reader["login"].ToString(), Password = reader["pass"].ToString(), Status = reader["status"].ToString() });
                    }
                }
            }
            return users;
        }
        public static User GetUserByLogin(string login)
        {
            User user = null;
            string expression = $@"SELECT Login as login, Password as pass, Status as status
                                FROM Users
                                WHERE Login = '{login}'";

            using (SqliteConnection sqliteConnection = new("Data Source=Data/TimeTableDB.db;Mode=ReadOnly"))
            {
                sqliteConnection.Open();
                SqliteCommand command = new(expression, sqliteConnection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            user = new User() { Login = reader["login"].ToString(), Password = reader["pass"].ToString(), Status = reader["status"].ToString() };
                    }
                }
            }
            return user;
        }
        public static void AddUser(User user)
        {
            string expression = $@"INSERT INTO Users (Login, Password, Status) VALUES ('{user.Login}', '{user.Password}', '{user.Status}')";

            using (SqliteConnection sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                SqliteCommand command = new(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateUser(User user, string login)
        {
            string expression = $@"UPDATE Users SET Login='{user.Login}', Password='{user.Password}', Status='{user.Status}' WHERE Login='{login}'";

            using (SqliteConnection sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                SqliteCommand command = new(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteUserByLogin(string login)
        {
            string expression = $@"DELETE FROM Users WHERE Login='{login}'";

            using (SqliteConnection sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                SqliteCommand command = new(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
