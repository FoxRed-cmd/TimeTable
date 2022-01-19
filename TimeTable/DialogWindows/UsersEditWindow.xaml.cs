using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TimeTable.Pages;

namespace TimeTable.DialogWindows
{
    public partial class UsersEditWindow : Window
    {
        private User user;
        private string currentUser;
        public UsersPage UsersPageModel { get; set; }
        public UsersEditWindow()
        {
            InitializeComponent();
            txtTitle.Text = "Добавить";
        }
        public UsersEditWindow(User user)
        {
            InitializeComponent();
            txtTitle.Text = "Редактировать";
            currentUser = user.Login;
            txtLogin.Text = user.Login;
            txtPass.Text = user.Password;
            txtStatus.Text = user.Status;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtTitle.Text == "Добавить")
            {
                if (txtLogin.Text != "" && txtPass.Text != "" && txtStatus.Text != "")
                {
                    try
                    {
                        user = new User()
                        {
                            Login = txtLogin.Text,
                            Password = txtPass.Text,
                            Status = txtStatus.Text
                        };
                        User.AddUser(user);
                        txtLogin.Clear();
                        txtPass.Clear();
                        txtStatus.Clear();
                        UsersPageModel.dataGridUsers.ItemsSource = User.GetAllDataFromTable();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                    System.Windows.MessageBox.Show("Поля: Логин, Пароль и Статус - обязательны к заполнению", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (txtLogin.Text != "" && txtPass.Text != "" && txtStatus.Text != "")
                {
                    try
                    {
                        user = new User()
                        {
                            Login = txtLogin.Text,
                            Password = txtPass.Text,
                            Status = txtStatus.Text
                        };
                        User.UpdateUser(user, currentUser);
                        UsersPageModel.dataGridUsers.ItemsSource = User.GetAllDataFromTable();
                        Student.UpdateStudentLogin(currentUser, user.Login);
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

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void Button_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
