using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Text.Json;

namespace TimeTable
{
    public class User
    {
        private static string? expression;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public static IEnumerable<User> GetAllDataFromTable()
        {
            expression = @"SELECT Login as login, Password as pass, Status as status
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
                            yield return new User() { Login = reader["login"].ToString(), Password = reader["pass"].ToString(), Status = reader["status"].ToString() };
                    }
                }
            }
        }
        public static User GetUserByLogin(string login)
        {
            expression = $@"SELECT Login as login, Password as pass, Status as status
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
                            return new User() { Login = reader["login"].ToString(), Password = reader["pass"].ToString(), Status = reader["status"].ToString() };
                    }
                }
            }
            return null;
        }
        public static void AddUser(User user)
        {
            expression = $@"INSERT INTO Users (Login, Password, Status) VALUES ('{user.Login}', '{user.Password}', '{user.Status}')";

            using (SqliteConnection sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                SqliteCommand command = new(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateUser(User user, string login)
        {
            expression = $@"UPDATE Users SET Login='{user.Login}', Password='{user.Password}', Status='{user.Status}' WHERE Login='{login}'";

            using (SqliteConnection sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                SqliteCommand command = new(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteUserByLogin(string login)
        {
            expression = $@"DELETE FROM Users WHERE Login='{login}'";

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
