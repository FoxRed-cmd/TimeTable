using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace TimeTable.DialogWindows
{
    public partial class RatingEditWindow : Window
    {
        private Rating currentRating;
        private string currentGroup;
        private MainDataViewModel viewModel;
        private List<ComboData> comboData = new List<ComboData>();
        public RatingEditWindow(MainDataViewModel viewModel)
        {
            InitializeComponent();
            txtTitle.Text = "Добавить";
            this.viewModel = viewModel;
            txtStudent.ItemsSource = comboData;

            txtStudent.MouseEnter += (s, e) =>
            {
                if (txtStudent.Text == string.Empty || currentGroup != txtGroup.Text)
                {
                    currentGroup = txtGroup.Text;
                    comboData.Clear();
                    txtStudent.ItemsSource = null;
                    List<Student> students = Student.GetStudentsByGroup(txtGroup.Text).ToList();
                    for (int i = 0, j = 0; i < students.Count; i++)
                    {
                        comboData.Add(new ComboData { Id = ++j, Value = students[i].Name });
                    }
                    txtStudent.ItemsSource = comboData;
                }
            };
        }

        public RatingEditWindow(Rating rating, MainDataViewModel viewModel)
        {
            InitializeComponent();
            txtTitle.Text = "Редактировать";
            this.viewModel = viewModel;
            this.currentRating = rating;

            txtDate.Text = rating.Date.ToString();
            txtGroup.Text = rating.GroupName;
            txtRating.Text = rating.Ratings.ToString();
            txtStudent.Text = rating.StudentName;
            txtSub.Text = rating.Subject;

            txtStudent.ItemsSource = comboData;

            txtStudent.MouseEnter += (s, e) =>
            {
                if (txtStudent.Text == string.Empty || currentGroup != txtGroup.Text)
                {
                    currentGroup = txtGroup.Text;
                    comboData.Clear();
                    txtStudent.ItemsSource = null;
                    List<Student> students = Student.GetStudentsByGroup(txtGroup.Text).ToList();
                    for (int i = 0, j = 0; i < students.Count; i++)
                    {
                        comboData.Add(new ComboData { Id = ++j, Value = students[i].Name });
                    }
                    txtStudent.ItemsSource = comboData;
                }
            };
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtTitle.Text.Equals("Добавить"))
            {
                if (txtGroup.Text.Equals(string.Empty) || txtSub.Text.Equals(string.Empty) || txtStudent.Text.Equals(string.Empty) || txtRating.Text.Equals(string.Empty))
                {
                    MessageBox.Show("Поля: Группа, Студент, Предмет и Оценка - обязательны к заполнению", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Rating rating = new Rating()
                {
                    GroupName = txtGroup.Text,
                    StudentName = txtStudent.Text,
                    Subject = txtSub.Text,
                    Ratings = txtRating.Text,
                    Date = DateTime.TryParse(txtDate.Text, out DateTime date) ? date : DateTime.Now
                };

                Rating.AddRating(rating);
                viewModel.Ratings = Rating.GetAllDataFromTable().ToList();
            }
            else
            {
                if (txtGroup.Text.Equals(string.Empty) || txtSub.Text.Equals(string.Empty) || txtStudent.Text.Equals(string.Empty) || txtRating.Text.Equals(string.Empty))
                {
                    MessageBox.Show("Поля: Группа, Студент, Предмет и Оценка - обязательны к заполнению", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Rating rating = new Rating()
                {
                    ID = currentRating.ID,
                    GroupName = txtGroup.Text,
                    StudentName = txtStudent.Text,
                    Subject = txtSub.Text,
                    Ratings = txtRating.Text,
                    Date = DateTime.TryParse(txtDate.Text, out DateTime date) ? date : DateTime.Now
                };

                Rating.UpdateRating(rating);
                viewModel.Ratings = Rating.GetAllDataFromTable().ToList();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => DragMove();
    }
}
