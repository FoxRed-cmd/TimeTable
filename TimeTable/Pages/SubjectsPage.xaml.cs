using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using TimeTable.DialogWindows;

namespace TimeTable.Pages
{
    public partial class SubjectsPage : Page
    {
        private SubjectEditWindow subjectEditWindow;
        public SubjectsPage()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            subjectEditWindow = new SubjectEditWindow(this.DataContext as MainDataViewModel);
            subjectEditWindow.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            SubjectModel subject = dataGridSubjects.SelectedItem as SubjectModel;
            if (subject != null)
            {
                subjectEditWindow = new SubjectEditWindow(subject, this.DataContext as MainDataViewModel);
                subjectEditWindow.ShowDialog();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var subjects = dataGridSubjects.SelectedItems;
            if (subjects.Count != 0)
            {
                if (DialogResult.OK == System.Windows.Forms.MessageBox.Show("Внимание! Удаляя предмет вы так же удалите его из расписаний! Вы уверены?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    try
                    {
                        foreach (var item in subjects)
                        {
                            SubjectModel subject = item as SubjectModel;
                            TimeTableModel.DeleteTimeTableBySubject(subject.SubjectName);
                            SubjectModel.DeleteSubjectByName(subject.SubjectName);
                        }
                        var context = this.DataContext as MainDataViewModel;
                        context.SubjectModels = SubjectModel.GetAllDataFromTable().ToList();
                    }
                    catch (System.Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
