using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Text.Json;

namespace TimeTable
{
    public class TimeTableModel
    {
        private static string? expression;
        private static SqliteConnection? sqliteConnection;
        private static SqliteCommand? command;
        private static SqliteDataReader? reader;
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Group { get; set; }
        public string DayOfWeek { get; set; }
        public string Time { get; set; }

        public static IEnumerable<TimeTableModel> GetAllDataFromTable()
        {
            expression = @"SELECT IDTable as id, Subject as subject, GroupName as groupName,
                                DayOfWeek as day, Time as time
                                FROM TimeTable";
            using (sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadOnly"))
            {
                sqliteConnection.Open();
                command = new(expression, sqliteConnection);
                using (reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            yield return new TimeTableModel()
                            {
                                Id = reader["id"].ToString() ?? string.Empty,
                                Subject = reader["subject"].ToString() ?? string.Empty,
                                Group = reader["groupName"].ToString() ?? string.Empty,
                                DayOfWeek = reader["day"].ToString() ?? string.Empty,
                                Time = reader["time"].ToString() ?? string.Empty
                            };
                    }
                }
            }
        }
        public static IEnumerable<TimeTableModel> GetTimeTableByGroup(string groupName)
        {
            expression = $@"SELECT IDTable as id, Subject as subject, GroupName as groupName,
                                DayOfWeek as day, Time as time
                                FROM TimeTable WHERE GroupName = '{groupName}'";
            using (sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadOnly"))
            {
                sqliteConnection.Open();
                command = new(expression, sqliteConnection);
                using (reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            yield return new TimeTableModel()
                            {
                                Id = reader["id"].ToString() ?? string.Empty,
                                Subject = reader["subject"].ToString() ?? string.Empty,
                                Group = reader["groupName"].ToString() ?? string.Empty,
                                DayOfWeek = reader["day"].ToString() ?? string.Empty,
                                Time = reader["time"].ToString() ?? string.Empty
                            };
                    }
                }
            }
        }
        public static void AddTimeTable(TimeTableModel timeTableModel)
        {
            expression = $@"INSERT INTO TimeTable (Subject, GroupName, DayOfWeek, Time) VALUES ('{timeTableModel.Subject}', '{timeTableModel.Group}', '{timeTableModel.DayOfWeek}', '{timeTableModel.Time}')";
            using (sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                command = new(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }
        public static void UpdateTimeTable(TimeTableModel timeTableModel, string currentID)
        {
            expression = $@"UPDATE TimeTable SET Subject='{timeTableModel.Subject}', GroupName='{timeTableModel.Group}', DayOfWeek='{timeTableModel.DayOfWeek}', Time='{timeTableModel.Time}' WHERE IDTable='{currentID}'";

            using (sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                command = new(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }
        public static void DeleteTimeTable(string id)
        {
            expression = $@"DELETE FROM TimeTable WHERE IDTable={id}";

            using (sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                command = new(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }
        public static void DeleteTimeTableBySubject(string subject)
        {
            expression = $@"DELETE FROM TimeTable WHERE Subject='{subject}'";

            using (sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                command = new(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }
        public static void DeleteTimeTableByGroup(string group)
        {
            expression = $@"DELETE FROM TimeTable WHERE GroupName='{group}'";

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
