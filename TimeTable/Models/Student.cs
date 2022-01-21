using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace TimeTable
{
    public class Student
    {
        private static string? expression;
        public string Login { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public byte[]? Photo { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public static IEnumerable<Student> GetAllDataFromTable()
        {
            expression = @"SELECT LoginOfStudents as login, GroupName as groupName, Name as name,
                                Photo as photo, Phone as phone, Email as email
                                FROM Students";
            using (SqliteConnection sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadOnly"))
            {
                sqliteConnection.Open();
                SqliteCommand command = new(expression, sqliteConnection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            yield return new Student()
                            {
                                Login = reader["login"].ToString() ?? string.Empty,
                                Group = reader["groupName"].ToString() ?? string.Empty,
                                Name = reader["Name"].ToString() ?? string.Empty,
                                Photo = Equals(reader["photo"], DBNull.Value) ? null : (byte[])reader["photo"],
                                Phone = reader["phone"].ToString(),
                                Email = reader["email"].ToString()
                            };
                    }
                }
            }
        }
        public static Student GetStudentByLogin(string login)
        {
            string expression = $@"SELECT LoginOfStudents as login, GroupName as groupName, Name as name,
                                Photo as photo, Phone as phone, Email as email
                                FROM Students 
                                WHERE LoginOfStudents = '{login}'";
            using (SqliteConnection sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadOnly"))
            {
                sqliteConnection.Open();
                SqliteCommand command = new(expression, sqliteConnection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            return new Student()
                            {
                                Login = reader["login"].ToString() ?? string.Empty,
                                Group = reader["groupName"].ToString() ?? string.Empty,
                                Name = reader["Name"].ToString() ?? string.Empty,
                                Photo = Equals(reader["photo"], DBNull.Value) ? null : (byte[])reader["photo"],
                                Phone = reader["phone"].ToString(),
                                Email = reader["email"].ToString()
                            };
                    }
                }
            }
            return null;
        }
        public static IEnumerable<Student> GetStudentsByGroup(string groupName)
        {
            expression = $@"SELECT LoginOfStudents as login, GroupName as groupName, Name as name,
                                Photo as photo, Phone as phone, Email as email
                                FROM Students 
                                WHERE GroupName = '{groupName}'";
            using (SqliteConnection sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadOnly"))
            {
                sqliteConnection.Open();
                SqliteCommand command = new(expression, sqliteConnection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            yield return new Student()
                            {
                                Login = reader["login"].ToString() ?? string.Empty,
                                Group = reader["groupName"].ToString() ?? string.Empty,
                                Name = reader["Name"].ToString() ?? string.Empty,
                                Photo = Equals(reader["photo"], DBNull.Value) ? null : (byte[])reader["photo"],
                                Phone = reader["phone"].ToString(),
                                Email = reader["email"].ToString()
                            };
                    }
                }
            }
        }
        public static void AddStudent(Student student)
        {
            if (student.Photo == null)
                expression = @$"INSERT INTO Students (LoginOfStudents, GroupName, Name, Photo, Phone, Email) VALUES ('{student.Login}', '{student.Group}', '{student.Name}', null, '{student.Phone}', '{student.Email}')";
            else if (student.Phone == null)
                expression = @$"INSERT INTO Students (LoginOfStudents, GroupName, Name, Photo, Phone, Email) VALUES ('{student.Login}', '{student.Group}', '{student.Name}', @photo, null, '{student.Email}')";
            else if (student.Email == null)
                expression = @$"INSERT INTO Students (LoginOfStudents, GroupName, Name, Photo, Phone, Email) VALUES ('{student.Login}', '{student.Group}', '{student.Name}', @photo, '{student.Phone}', null)";
            else if (student.Photo == null && student.Phone == null && student.Email == null)
                expression = @$"INSERT INTO Students (LoginOfStudents, GroupName, Name, Photo, Phone, Email) VALUES ('{student.Login}', '{student.Group}', '{student.Name}', null, null, null)";
            else
                expression = @$"INSERT INTO Students (LoginOfStudents, GroupName, Name, Photo, Phone, Email) VALUES ('{student.Login}', '{student.Group}', '{student.Name}', @photo, '{student.Phone}', '{student.Email}')";


            using (SqliteConnection sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                SqliteCommand command = new(expression, sqliteConnection);
                if (student.Photo != null)
                    command.Parameters.Add("@photo", Microsoft.Data.Sqlite.SqliteType.Blob, student.Photo.Length).Value = student.Photo;
                command.ExecuteNonQuery();
            }
        }
        public static void UpdateStudent(Student student, string login)
        {
            if (student.Photo == null)
                expression = @$"UPDATE Students SET LoginOfStudents='{student.Login}', GroupName='{student.Group}', Name='{student.Name}', Phone='{student.Phone}', Email='{student.Email}' WHERE LoginOfStudents='{login}'";
            else if (student.Phone == null)
                expression = @$"UPDATE Students SET LoginOfStudents='{student.Login}', GroupName='{student.Group}', Name='{student.Name}', Photo=@photo, Phone=null, Email='{student.Email}' WHERE LoginOfStudents='{login}'";
            else if (student.Email == null)
                expression = @$"UPDATE Students SET LoginOfStudents='{student.Login}', GroupName='{student.Group}', Name='{student.Name}', Photo=@photo, Phone='{student.Phone}', Email=null WHERE LoginOfStudents='{login}'";
            else if (student.Photo == null && student.Email == null && student.Phone == null)
                expression = @$"UPDATE Students SET LoginOfStudents='{student.Login}', GroupName='{student.Group}', Name='{student.Name}', Photo=null, Phone=null, Email=null WHERE LoginOfStudents='{login}'";
            else
                expression = @$"UPDATE Students SET LoginOfStudents='{student.Login}', GroupName='{student.Group}', Name='{student.Name}', Photo=@photo, Phone='{student.Phone}', Email='{student.Email}' WHERE LoginOfStudents='{login}'";

            using (SqliteConnection sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                SqliteCommand command = new(expression, sqliteConnection);
                if (student.Photo != null)
                    command.Parameters.Add("@photo", Microsoft.Data.Sqlite.SqliteType.Blob, student.Photo.Length).Value = student.Photo;
                command.ExecuteNonQuery();
            }
        }
        public static void UpdateStudentLogin(string oldLogin, string newLogin)
        {
            expression = $@"UPDATE Students SET LoginOfStudents='{newLogin}' WHERE LoginOfStudents='{oldLogin}'";
            using (SqliteConnection sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                SqliteCommand command = new(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }
        public static void DeleteStudentByLogin(string login)
        {
            expression = $@"DELETE FROM Students WHERE LoginOfStudents='{login}'";
            using (SqliteConnection sqliteConnection = new SqliteConnection("Data Source=Data/TimeTableDB.db;Mode=ReadWrite"))
            {
                sqliteConnection.Open();
                SqliteCommand command = new SqliteCommand(expression, sqliteConnection);
                command.ExecuteNonQuery();
            }
        }
        public static void DeleteStudentByGroup(string groupName)
        {
            GetStudentsByGroup(groupName).ToList().ForEach(student => User.DeleteUserByLogin(student.Login));
            expression = $@"DELETE FROM Students WHERE GroupName='{groupName}'";
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
