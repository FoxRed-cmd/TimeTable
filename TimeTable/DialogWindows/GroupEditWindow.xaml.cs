using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace TimeTable.DialogWindows
{
    public partial class GroupEditWindow : Window
    {
        private Group group;
        private string currentGroup;
        MainDataViewModel viewModel;
        public GroupEditWindow(MainDataViewModel viewModel)
        {
            InitializeComponent();
            txtTitle.Text = "Добавить";
            this.viewModel = viewModel;
        }

        public GroupEditWindow(Group group, MainDataViewModel viewModel)
        {
            InitializeComponent();
            txtTitle.Text = "Редактировать";
            currentGroup = group.Name;
            txtGroup.Text = group.Name;
            txtDesc.Text = group.Description;
            txtTime.Text = group.TrainingPeriod;
            txtForm.Text = group.FormOfStudy;
            this.viewModel = viewModel;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();
        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtTitle.Text == "Добавить")
            {
                if (txtGroup.Text != "" && txtTime.Text != "" && txtForm.Text != "")
                {
                    try
                    {
                        group = new Group()
                        {
                            Name = txtGroup.Text,
                            Description = txtDesc.Text,
                            TrainingPeriod = txtTime.Text,
                            FormOfStudy = txtForm.Text,
                        };
                        Group.AddGroup(group);
                        txtGroup.Clear();
                        txtDesc.Clear();
                        txtTime.Clear();
                        txtForm.Clear();
                        viewModel.Groups = Group.GetAllDataFromTable().ToList();

                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                    System.Windows.MessageBox.Show("Поля: Группа, Время обучения и Форма обучения - обязательны к заполнению", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (txtGroup.Text != "" && txtTime.Text != "" && txtForm.Text != "")
                {
                    try
                    {
                        group = new Group()
                        {
                            Name = txtGroup.Text,
                            Description = txtDesc.Text,
                            TrainingPeriod = txtTime.Text,
                            FormOfStudy = txtForm.Text,
                        };
                        Group.UpdateGroup(group, currentGroup);
                        viewModel.Groups = Group.GetAllDataFromTable().ToList();
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
