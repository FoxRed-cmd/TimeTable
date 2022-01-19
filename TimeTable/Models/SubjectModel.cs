using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    public class SubjectModel
    {
        public string SubjectName { get; set; }
        public string Description { get; set; }
        public string TeacherName { get; set; }

        public static IEnumerable<SubjectModel> GetAllDataFromTable()
        {
            List<SubjectModel> subjects = new();
            string expression = @"SELECT SubjectName as name, Description as desc, TeacherName as tn
                                FROM Subject";
            using (SqliteConnection sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadOnly"))
            {
                sqliteConnection.Open();
                SqliteCommand command = new(expression, sqliteConnection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            subjects.Add(new SubjectModel()
                            {
                                SubjectName = reader["name"].ToString() ?? string.Empty,
                                Description = reader["desc"].ToString() ?? string.Empty,
                                TeacherName = reader["tn"].ToString() ?? string.Empty,
                            });
                    }
                }
            }
            return subjects;
        }
        public static void AddSubject(SubjectModel subject)
        {
            string expression;
            if (subject.Description != null)
                expression = $@"INSERT INTO Subject (SubjectName, Description, TeacherName) VALUES ('{subject.SubjectName}', '{subject.Description}', '{subject.TeacherName}')";
            else
                expression = $@"INSERT INTO Subject (SubjectName, Description, TeacherName) VALUES ('{subject.SubjectName}', null, '{subject.TeacherName}')";

            using (SqliteConnection sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                SqliteCommand command = new(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }
        public static void UpdateSubject(SubjectModel subject, string currentSub)
        {
            string expression;
            if (subject.Description != null)
                expression = $@"UPDATE Subject SET SubjectName='{subject.SubjectName}', Description='{subject.Description}', TeacherName='{subject.TeacherName}' WHERE SubjectName='{currentSub}'";
            else
                expression = $@"UPDATE Subject SET SubjectName='{subject.SubjectName}', Description=null, TeacherName='{subject.TeacherName}' WHERE SubjectName='{currentSub}'";

            using (SqliteConnection sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                SqliteCommand command = new(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }
        public static void DeleteSubjectByName(string name)
        {
            string expression = $@"DELETE FROM Subject WHERE SubjectName='{name}'";
            using (SqliteConnection sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                SqliteCommand command = new SqliteCommand(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }
    }
}
