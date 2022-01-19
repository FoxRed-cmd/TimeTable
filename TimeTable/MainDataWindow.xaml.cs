using System.Windows;
using System.Windows.Input;

namespace TimeTable
{
    public partial class MainDataWindow : Window
    {
        private MainWindow? mainWindow = null;
        private bool _isShow = false;
        public MainDataWindow(User user)
        {
            InitializeComponent();
            boxUser.Text = $"Пользователь: {user?.Login}";
            boxStatus.Text = $"Статус: {user?.Status}";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow = Owner as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.txtLogin.Text = "Login";
                mainWindow.txtPW.Password = "Password";
                mainWindow.Show();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void animButton_Click(object sender, RoutedEventArgs e)
        {
            switch (_isShow)
            {
                case false:
                    showAnim.Storyboard.Begin();
                    _isShow = !_isShow;
                    break;
                case true:
                    hideAnim.Storyboard.Begin();
                    _isShow = !_isShow;
                    break;
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            switch (_isShow)
            {
                case false:
                    showAnim.Storyboard.Begin();
                    _isShow = !_isShow;
                    break;
                case true:
                    hideAnim.Storyboard.Begin();
                    _isShow = !_isShow;
                    break;
            }
        }
    }
}
