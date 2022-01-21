using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Text.Json;

namespace TimeTable
{
    public class Group
    {
        private static string? expression;
        public string Name { get; set; }
        public string? Description { get; set; }
        public string TrainingPeriod { get; set; }
        public string FormOfStudy { get; set; }

        public static IEnumerable<Group> GetAllDataFromTable()
        {
            expression = @"SELECT GroupName as name, Description as description, TrainingPeriod as trainingPeriod,
                                FormOfStudy as formOfStudy
                                FROM Groups";
            using (SqliteConnection sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadOnly"))
            {
                sqliteConnection.Open();
                SqliteCommand command = new(expression, sqliteConnection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            yield return new Group
                            {
                                Name = reader["name"].ToString() ?? string.Empty,
                                Description = reader["description"].ToString() ?? null,
                                TrainingPeriod = reader["trainingPeriod"].ToString() ?? string.Empty,
                                FormOfStudy = reader["formOfStudy"].ToString() ?? string.Empty,
                            };
                    }
                }
            }
        }
        public static void AddGroup(Group group)
        {
            if (group.Description != null)
                expression = $@"INSERT INTO Groups (GroupName, Description, TrainingPeriod, FormOfStudy) VALUES ('{group.Name}', '{group.Description}', '{group.TrainingPeriod}', '{group.FormOfStudy}')";
            else
                expression = $@"INSERT INTO Groups (GroupName, Description, TrainingPeriod, FormOfStudy) VALUES ('{group.Name}', null, '{group.TrainingPeriod}', '{group.FormOfStudy}')";

            using (SqliteConnection sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                SqliteCommand command = new(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }
        public static void UpdateGroup(Group group, string currentGroup)
        {
            if (group.Description != null)
                expression = $@"UPDATE Groups SET GroupName='{group.Name}', Description='{group.Description}', TrainingPeriod='{group.TrainingPeriod}', FormOfStudy='{group.FormOfStudy}' WHERE GroupName='{currentGroup}'";
            else
                expression = $@"UPDATE Groups SET GroupName='{group.Name}', Description=null, TrainingPeriod='{group.TrainingPeriod}', FormOfStudy='{group.FormOfStudy}' WHERE GroupName='{currentGroup}'";

            using (SqliteConnection sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                SqliteCommand command = new(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }
        public static void DeleteGroupByName(string groupName)
        {
            expression = $@"DELETE FROM Groups WHERE GroupName='{groupName}'";
            using (SqliteConnection sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                SqliteCommand command = new SqliteCommand(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
