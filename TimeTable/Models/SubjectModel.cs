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
    }
}
