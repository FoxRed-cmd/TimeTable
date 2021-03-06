using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using TimeTable.DialogWindows;

namespace TimeTable.Pages
{
    public partial class StudentsPage : Page
    {
        private StudentEditWindow studentEditWindow;
        public StudentsPage()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            studentEditWindow = new StudentEditWindow(this.DataContext as MainDataViewModel);
            studentEditWindow.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Student? student = dataGridStudents.SelectedItem as Student;
            if (student != null)
            {
                studentEditWindow = new StudentEditWindow(student, this.DataContext as MainDataViewModel);
                studentEditWindow.ShowDialog();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var students = dataGridStudents.SelectedItems;
            if (students.Count != 0)
            {
                if (DialogResult.OK == System.Windows.Forms.MessageBox.Show("Выбранные объекты будут удалены! Вы уверены?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    try
                    {
                        foreach (var item in students)
                        {
                            Student student = item as Student;
                            Student.DeleteStudentByLogin(student.Login);
                            User.DeleteUserByLogin(student.Login);
                        }
                        var context = this.DataContext as MainDataViewModel;
                        context.Students = Student.GetAllDataFromTable().ToList();
                    }
                    catch (System.Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => (this.DataContext as MainDataViewModel).Students = Student.GetAllDataFromTable().ToList();
    }
}
