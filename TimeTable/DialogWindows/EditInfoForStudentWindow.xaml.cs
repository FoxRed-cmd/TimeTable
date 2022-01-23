using System;
using System.Windows;
using System.Windows.Input;
using TimeTable.Pages;

namespace TimeTable.DialogWindows
{ 
    public partial class EditInfoForStudentWindow : Window
    {
        private StudentInfoPage studentInfoPage;
        private Student newStudent;
        private User user;
        private Student currentStudent;
        public EditInfoForStudentWindow(Student student, StudentInfoPage studentInfoPage)
        {
            InitializeComponent();
            this.studentInfoPage = studentInfoPage; 
            currentStudent = student;
            txtLogin.Text = student.Login;
            txtPass.Text = User.GetUserByLogin(student.Login).Password;
            txtPhone.Text = student.Phone;
            txtEmail.Text = student.Email;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void Button_Click(object sender, RoutedEventArgs e) => Close();

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtLogin.Text != "" && txtPass.Text != "")
            {
                try
                {
                    newStudent = new Student()
                    {
                        Name = currentStudent.Name,
                        Email = txtEmail.Text,
                        Group = currentStudent.Group,
                        Login = txtLogin.Text,
                        Phone = txtPhone.Text,
                        Photo = currentStudent.Photo,
                    };
                    user = new User()
                    {
                        Login = txtLogin.Text,
                        Password = txtPass.Text,
                        Status = "Student",
                    };
                    Student.UpdateStudent(newStudent, currentStudent.Login);
                    User.UpdateUser(user, currentStudent.Login);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка обновления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Логин и Пароль обязательны к заполнению!", "Ошибка обновления", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            if (newStudent != null)
                studentInfoPage.LoadingStudent(newStudent);
        }
    }
}
