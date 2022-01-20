using System.Windows;
using System.Windows.Input;
using TimeTable.Pages;

namespace TimeTable
{
    public partial class StudentMainWindow : Window
    {
        private MainWindow? mainWindow = null;
        private bool _isShow = false;
        private string login;
        public StudentInfoPage StudentInfo { get; set; }
        public TimeTableForStudentInfoPage TimeTableForStudentInfo { get; set; }
        public StudentMainWindow(User user)
        {
            InitializeComponent();
            boxUser.Text = $"Пользователь: {user?.Login}";
            login = user?.Login;
            boxStatus.Text = $"Статус: {user?.Status}";
        }

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (StudentInfo != null)
            {
                if (MainFrame.Content is not StudentInfoPage)
                {
                    MainFrame.Content = StudentInfo;
                }
            }
            else
            {
                if (MainFrame.Content is not StudentInfoPage)
                {
                    StudentInfo = new StudentInfoPage(Student.GetStudentByLogin(login));
                    MainFrame.Content = StudentInfo;
                }
            }

            if (_isShow == true)
            {
                animButton_Click(sender, e);
            }

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

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (TimeTableForStudentInfo != null)
            {
                if (MainFrame.Content is not TimeTableForStudentInfoPage)
                {
                    MainFrame.Content = TimeTableForStudentInfo;
                }
            }
            else
            {
                if (MainFrame.Content is not TimeTableForStudentInfoPage)
                {
                    TimeTableForStudentInfo = new TimeTableForStudentInfoPage(Student.GetStudentByLogin(login));
                    MainFrame.Content = TimeTableForStudentInfo;
                }
            }

            if (_isShow == true)
            {
                animButton_Click(sender, e);
            }
        }
    }
}
