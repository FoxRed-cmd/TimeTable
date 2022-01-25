using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace TimeTable.DialogWindows
{

    public partial class SubjectEditWindow : Window
    {
        private SubjectModel subjectModel;
        private string currentSubject;
        private MainDataViewModel viewModel;
        public SubjectEditWindow(MainDataViewModel viewModel)
        {
            InitializeComponent();
            txtTitle.Text = "Добавить";
            this.viewModel = viewModel;
        }
        public SubjectEditWindow(SubjectModel subject, MainDataViewModel viewModel)
        {
            InitializeComponent();
            txtTitle.Text = "Редактировать";
            currentSubject = subject.SubjectName;
            txtSub.Text = subject.SubjectName;
            txtDesc.Text = subject.Description;
            txtTeacher.Text = subject.TeacherName;
            this.viewModel = viewModel;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void Button_Click(object sender, RoutedEventArgs e) => this.Close();

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtTitle.Text == "Добавить")
            {
                if (txtSub.Text != "" && txtTeacher.Text != "")
                {
                    try
                    {
                        subjectModel = new SubjectModel()
                        {
                            SubjectName = txtSub.Text,
                            Description = txtDesc.Text,
                            TeacherName = txtTeacher.Text,
                        };
                        SubjectModel.AddSubject(subjectModel);
                        txtTeacher.Clear();
                        txtSub.Clear();
                        txtDesc.Clear();
                        viewModel.SubjectModels = SubjectModel.GetAllDataFromTable().ToList();

                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                    System.Windows.MessageBox.Show("Поля: Предмет и Преподаватель - обязательны к заполнению", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (txtSub.Text != "" && txtTeacher.Text != "")
                {
                    try
                    {
                        subjectModel = new SubjectModel()
                        {
                            SubjectName = txtSub.Text,
                            Description = txtDesc.Text,
                            TeacherName = txtTeacher.Text,
                        };
                        SubjectModel.UpdateSubject(subjectModel, currentSubject);
                        viewModel.SubjectModels = SubjectModel.GetAllDataFromTable().ToList();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка обновления", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                    System.Windows.MessageBox.Show("Поля: Группа, Время обучения и Форма обучения - обязательны к заполнению", "Ошибка обновления", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
