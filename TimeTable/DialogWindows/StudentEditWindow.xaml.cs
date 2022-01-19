using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using TimeTable.Pages;

namespace TimeTable.DialogWindows
{
    public partial class StudentEditWindow : Window
    {
        private Student student;
        private User user;
        private string currentLogin;
        private byte[] photo;
        internal StudentsPage StudentPage { get; set; }
        public StudentEditWindow()
        {
            InitializeComponent();
            txtTitle.Text = "Добавить";
        }
        public StudentEditWindow(Student student)
        {
            InitializeComponent();
            txtTitle.Text = "Редактировать";
            currentLogin = student.Login;
            txtLogin.Text = student.Login;
            txtIDGroup.Text = student.Group.ToString();
            txtName.Text = student.Name;
            txtPhone.Text = student.Phone;
            txtEmail.Text = student.Email;
            txtPass.Text = User.GetUserByLogin(student.Login).Password;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void BtnEditPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Title = "Выберите фото",
                Filter = "png files (*.png)|*.png|jpg files (*.jpg)|*.jpg",
                Multiselect = false,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            };
            if (System.Windows.Forms.DialogResult.OK == openFile.ShowDialog())
            {
                photo = File.ReadAllBytes(openFile.FileName);
            };
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtTitle.Text == "Добавить")
            {
                if (txtLogin.Text != "" && txtName.Text != "" && txtIDGroup.Text != "")
                {
                    try
                    {
                        student = new Student()
                        {
                            Login = txtLogin.Text,
                            Group = txtIDGroup.Text,
                            Name = txtName.Text,
                            Photo = photo ?? null,
                            Phone = txtPhone.Text,
                            Email = txtEmail.Text,
                        };
                        Student.AddStudent(student);
                        user = new User()
                        {
                            Login = txtLogin.Text,
                            Password = txtPass.Text,
                            Status = "Student"
                        };
                        User.AddUser(user);
                        txtEmail.Clear();
                        txtName.Clear();
                        txtLogin.Clear();
                        txtPhone.Clear();
                        txtPass.Clear();
                        StudentPage.dataGridStudents.ItemsSource = Student.GetAllDataFromTable();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                    System.Windows.MessageBox.Show("Поля: Логин, Код группы и ФИО - обязательны к заполнению", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (txtLogin.Text != "" && txtName.Text != "" && txtIDGroup.Text != "")
                {
                    try
                    {
                        student = new Student()
                        {
                            Login = txtLogin.Text,
                            Group = txtIDGroup.Text,
                            Name = txtName.Text,
                            Photo = photo ?? null,
                            Phone = txtPhone.Text,
                            Email = txtEmail.Text,
                        };
                        Student.UpdateStudent(student, currentLogin);
                        user = new User()
                        {
                            Login = txtLogin.Text,
                            Password = txtPass.Text,
                            Status = "Student"
                        };
                        User.UpdateUser(user, currentLogin);
                        StudentPage.dataGridStudents.ItemsSource = Student.GetAllDataFromTable().ToList();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка обновления", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                    System.Windows.MessageBox.Show("Поля: Логин, Код группы и ФИО - обязательны к заполнению", "Ошибка обновления", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
