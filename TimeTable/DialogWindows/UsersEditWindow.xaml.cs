using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace TimeTable.DialogWindows
{
    public partial class UsersEditWindow : Window
    {
        private User user;
        private string currentUser;
        private MainDataViewModel viewModel;
        public UsersEditWindow(MainDataViewModel viewModel)
        {
            InitializeComponent();
            txtTitle.Text = "Добавить";
            this.viewModel = viewModel;
        }
        public UsersEditWindow(User user, MainDataViewModel viewModel)
        {
            InitializeComponent();
            txtTitle.Text = "Редактировать";
            currentUser = user.Login;
            txtLogin.Text = user.Login;
            txtPass.Text = user.Password;
            txtStatus.Text = user.Status;
            this.viewModel = viewModel;
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
                        viewModel.Users = User.GetAllDataFromTable().ToList();
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
                        Student.UpdateStudentLogin(currentUser, user.Login);
                        viewModel.Users = User.GetAllDataFromTable().ToList();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка обновления", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                    System.Windows.MessageBox.Show("Поля: Логин, Пароль и Статус - обязательны к заполнению", "Ошибка обновления", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void Button_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
