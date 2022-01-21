using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Text.Json;

namespace TimeTable
{
    public class User
    {
        private static string? expression;
        private static SqliteConnection? sqliteConnection;
        private static SqliteCommand? command;
        private static SqliteDataReader? reader;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public static IEnumerable<User> GetAllDataFromTable()
        {
            expression = @"SELECT Login as login, Password as pass, Status as status
                                FROM Users";

            using (sqliteConnection = new("Data Source=Data/TimeTableDB.db;Mode=ReadOnly"))
            {
                sqliteConnection.Open();
                command = new(expression, sqliteConnection);
                using (reader = command.ExecuteReader())
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

            using (sqliteConnection = new("Data Source=Data/TimeTableDB.db;Mode=ReadOnly"))
            {
                sqliteConnection.Open();
                command = new(expression, sqliteConnection);
                using (reader = command.ExecuteReader())
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

            using (sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                command = new(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateUser(User user, string login)
        {
            expression = $@"UPDATE Users SET Login='{user.Login}', Password='{user.Password}', Status='{user.Status}' WHERE Login='{login}'";

            using (sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                command = new(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteUserByLogin(string login)
        {
            expression = $@"DELETE FROM Users WHERE Login='{login}'";

            using (sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                command = new(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
