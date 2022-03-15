using System.Linq;
using System.Windows.Controls;

namespace TimeTable.Pages
{
    public partial class RatingForStudentPage : Page
    {
        public RatingForStudentPage(Student student)
        {
            InitializeComponent();
            dataGridGroups.ItemsSource = Rating.GetRatingsByStudentLogin(student.Login).ToList();
        }
    }
}
