using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace TimeTable
{
    public class Rating
    {
        private static string? expression;
        private static SqliteConnection? sqliteConnection;
        private static SqliteCommand? command;
        private static SqliteDataReader? reader;

        public int ID { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Ratings { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;

        public static IEnumerable<Rating> GetAllDataFromTable()
        {
            expression = @"SELECT ID as id, GroupName as name, StudentName as student, Subject as subject,
                                Rating as rating, Date as date
                                FROM Ratings";
            using (sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadOnly"))
            {
                sqliteConnection.Open();
                command = new(expression, sqliteConnection);
                using (reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            yield return new Rating
                            {
                                ID = int.Parse(reader["id"].ToString()),
                                GroupName = reader["name"].ToString() ?? string.Empty,
                                StudentName = Student.GetStudentByLogin(reader["student"].ToString()).Name ?? string.Empty,
                                Subject = reader["subject"].ToString() ?? string.Empty,
                                Ratings = reader["rating"].ToString() ?? string.Empty,
                                Date = DateTime.Parse(reader["date"].ToString())
                            };
                    }
                }
            }
        }

        public static IEnumerable<Rating> GetRatingsByStudentLogin(string studentLogin)
        {
            expression = @$"SELECT ID as id, GroupName as name, StudentName as student, Subject as subject,
                         Rating as rating, Date as date
                         FROM Ratings
                         WHERE StudentName='{studentLogin}'";
            using (sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadOnly"))
            {
                sqliteConnection.Open();
                command = new(expression, sqliteConnection);
                using (reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            yield return new Rating
                            {
                                ID = int.Parse(reader["id"].ToString()),
                                GroupName = reader["name"].ToString() ?? string.Empty,
                                StudentName = Student.GetStudentByLogin(reader["student"].ToString()).Name ?? string.Empty,
                                Subject = reader["subject"].ToString() ?? string.Empty,
                                Ratings = reader["rating"].ToString() ?? string.Empty,
                                Date = DateTime.Parse(reader["date"].ToString())
                            };
                    }
                }
            }
        }

        public static void AddRating(Rating rating)
        {
            expression = $@"INSERT INTO Ratings 
            (GroupName, StudentName, Subject, Rating, Date)
            VALUES ('{rating.GroupName}', '{Student.GetStudentByName(rating.StudentName).Login}', '{rating.Subject}', {rating.Ratings}, '{rating.Date}')";

            using (sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                command = new(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateRating(Rating rating)
        {
            expression = $@"UPDATE Ratings SET GroupName='{rating.GroupName}', StudentName='{Student.GetStudentByName(rating.StudentName).Login}',
            Subject='{rating.Subject}', Rating={rating.Ratings}, Date='{rating.Date}' WHERE ID={rating.ID}";
            using (sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                command = new(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateRatingLogin(string oldLogin, string newLogin)
        {
            expression = $@"UPDATE Ratings SET StudentName='{newLogin}' WHERE StudentName='{oldLogin}'";
            using (sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                command = new(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteRating(Rating rating)
        {
            expression = $@"DELETE FROM Ratings WHERE ID={rating.ID}";

            using (sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                command = new(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }
    }
}
