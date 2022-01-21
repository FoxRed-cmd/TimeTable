using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace TimeTable.Pages
{
    public partial class TimeTableForStudentInfoPage : Page
    {
        public TimeTableForStudentInfoPage(Student student)
        {
            InitializeComponent();
            List<TimeTableModel> timeTable = TimeTableModel.GetTimeTableByGroup(student.Group).ToList();
            if (timeTable.Count != 0)
            {
                foreach (var item in timeTable)
                {
                    if (item.DayOfWeek.ToLower() == "понедельник")
                    {
                        switch (item.Time)
                        {
                            case "8:30": M1.Text += item.Subject; break;
                            case "10:10": M2.Text += item.Subject; break;
                            case "12:00": M3.Text += item.Subject; break;
                            case "14:00": M4.Text += item.Subject; break;
                            case "15:50": M5.Text += item.Subject; break;
                            case "17:40": M6.Text += item.Subject; break;
                            case "19:20": M7.Text += item.Subject; break;
                            default:
                                break;
                        }
                    }
                    else if (item.DayOfWeek.ToLower() == "вторник")
                    {
                        switch (item.Time)
                        {
                            case "8:30": T1.Text += item.Subject; break;
                            case "10:10": T2.Text += item.Subject; break;
                            case "12:00": T3.Text += item.Subject; break;
                            case "14:00": T4.Text += item.Subject; break;
                            case "15:50": T5.Text += item.Subject; break;
                            case "17:40": T6.Text += item.Subject; break;
                            case "19:20": T7.Text += item.Subject; break;
                            default:
                                break;
                        }
                    }
                    else if (item.DayOfWeek.ToLower() == "среда")
                    {
                        switch (item.Time)
                        {
                            case "8:30": W1.Text += item.Subject; break;
                            case "10:10": W2.Text += item.Subject; break;
                            case "12:00": W3.Text += item.Subject; break;
                            case "14:00": W4.Text += item.Subject; break;
                            case "15:50": W5.Text += item.Subject; break;
                            case "17:40": W6.Text += item.Subject; break;
                            case "19:20": W7.Text += item.Subject; break;
                            default:
                                break;
                        }
                    }
                    else if (item.DayOfWeek.ToLower() == "четверг")
                    {
                        switch (item.Time)
                        {
                            case "8:30": TH1.Text += item.Subject; break;
                            case "10:10": TH2.Text += item.Subject; break;
                            case "12:00": TH3.Text += item.Subject; break;
                            case "14:00": TH4.Text += item.Subject; break;
                            case "15:50": TH5.Text += item.Subject; break;
                            case "17:40": TH6.Text += item.Subject; break;
                            case "19:20": TH7.Text += item.Subject; break;
                            default:
                                break;
                        }
                    }
                    else if (item.DayOfWeek.ToLower() == "пятница")
                    {
                        switch (item.Time)
                        {
                            case "8:30": F1.Text += item.Subject; break;
                            case "10:10": F2.Text += item.Subject; break;
                            case "12:00": F3.Text += item.Subject; break;
                            case "14:00": F4.Text += item.Subject; break;
                            case "15:50": F5.Text += item.Subject; break;
                            case "17:40": F6.Text += item.Subject; break;
                            case "19:20": F7.Text += item.Subject; break;
                            default:
                                break;
                        }
                    }
                    else if (item.DayOfWeek.ToLower() == "суббота")
                    {
                        switch (item.Time)
                        {
                            case "8:30": S1.Text += item.Subject; break;
                            case "10:10": S2.Text += item.Subject; break;
                            case "12:00": S3.Text += item.Subject; break;
                            case "14:00": S4.Text += item.Subject; break;
                            case "15:50": S5.Text += item.Subject; break;
                            case "17:40": S6.Text += item.Subject; break;
                            case "19:20": S7.Text += item.Subject; break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}
