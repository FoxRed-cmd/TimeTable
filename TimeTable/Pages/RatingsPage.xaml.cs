using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using TimeTable.DialogWindows;

namespace TimeTable.Pages
{
    public partial class RatingsPage : Page
    {
        private RatingEditWindow ratingEditWindow;
        private Rating rating;
        public RatingsPage()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ratingEditWindow = new RatingEditWindow(this.DataContext as MainDataViewModel);
            ratingEditWindow.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            rating = dataGridGroups.SelectedItem as Rating;
            if (rating != null)
            {
                ratingEditWindow = new RatingEditWindow(rating, this.DataContext as MainDataViewModel);
                ratingEditWindow.ShowDialog();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var ratings = dataGridGroups.SelectedItems;
            if (ratings.Count != 0)
            {
                if (DialogResult.OK == System.Windows.Forms.MessageBox.Show("Выбранные объекты будут удалены! Вы уверены?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    try
                    {
                        foreach (var item in ratings)
                        {
                            rating = item as Rating;
                            Rating.DeleteRating(rating);
                        }

                        (this.DataContext as MainDataViewModel).Ratings = Rating.GetAllDataFromTable().ToList();
                    }
                    catch (System.Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => (this.DataContext as MainDataViewModel).Ratings = Rating.GetAllDataFromTable().ToList();
    }
}
