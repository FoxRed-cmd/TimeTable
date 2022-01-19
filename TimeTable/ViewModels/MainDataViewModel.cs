using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using TimeTable.Pages;

namespace TimeTable
{
    internal class ComboData
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
    internal class MainDataViewModel : ViewModel
    {
        private Page page;
        private List<ComboData> comboDatasGroup = new List<ComboData>();
        private List<ComboData> comboDatasSubject = new List<ComboData>();
        public Page Page
        {
            get { return page; }
            set { Set(ref page, value); }
        }
        public StudentsPage StudentsPageProp { get; set; }
        public GroupsPage GroupsPageProp { get; set; }
        public TimeTablesPage TimeTablesPageProp { get; set; }
        public List<ComboData> ComboDataGroup { get; set; }
        public List<ComboData> ComboDataSubject { get; set; }
        public List<Student> Students { get; set; }
        public List<Group> Groups { get; set; }
        public List<TimeTableModel> TimeTableModels { get; set; }
        public List<SubjectModel> SubjectModels { get; set; }

        public MainDataViewModel()
        {
            Page = new WelcomePage();
            Students = Student.GetAllDataFromTable().ToList();
            Groups = Group.GetAllDataFromTable().ToList();
            TimeTableModels = TimeTableModel.GetAllDataFromTable().ToList();
            SubjectModels = SubjectModel.GetAllDataFromTable().ToList();
            for (int i = 0, j = 0; i < Groups.Count; i++)
            {
                comboDatasGroup.Add(new ComboData { Id = ++j, Value = Groups[i].Name });
            }
            for (int i = 0, j = 0; i < SubjectModels.Count; i++)
            {
                comboDatasSubject.Add(new ComboData { Id = ++j, Value = SubjectModels[i].SubjectName });
            }
            
            ComboDataGroup = comboDatasGroup;
            ComboDataSubject = comboDatasSubject;

            OpenStudentsPageCommand = new LambdaCommand(OnOpenStudentsPageCommandExecuted, CanOpenStudentsPageCommandExecute);
            OpenGroupsPageCommand = new LambdaCommand(OnOpenGroupsPageCommandExecuted, CanOpenGroupsPageCommandExecute);
            OpenTimeTablesPageCommand = new LambdaCommand(OnOpenTimeTablesPageCommandExecuted, CanOpenTimeTablesPageCommandExecute);
        }

        #region Command
        public ICommand OpenStudentsPageCommand { get; }
        private bool CanOpenStudentsPageCommandExecute(object p)
        {
            return true;
        }
        private void OnOpenStudentsPageCommandExecuted(object p)
        {
            if (Page is not StudentsPage)
            {
                if (StudentsPageProp != null)
                    Page = StudentsPageProp;
                else
                {
                    StudentsPageProp = new StudentsPage();
                    Page = StudentsPageProp;
                }
            }
        }

        public ICommand OpenGroupsPageCommand { get; }
        private bool CanOpenGroupsPageCommandExecute(object p)
        {
            return true;
        }
        private void OnOpenGroupsPageCommandExecuted(object p)
        {
            if (Page is not GroupsPage)
            {
                if (GroupsPageProp != null)
                    Page = GroupsPageProp;
                else
                {
                    GroupsPageProp = new GroupsPage();
                    Page = GroupsPageProp;
                }
            }
        }

        public ICommand OpenTimeTablesPageCommand { get; }
        private bool CanOpenTimeTablesPageCommandExecute(object p)
        {
            return true;
        }
        private void OnOpenTimeTablesPageCommandExecuted(object p)
        {
            if (Page is not TimeTablesPage)
            {
                if (TimeTablesPageProp != null)
                    Page = TimeTablesPageProp;
                else
                {
                    TimeTablesPageProp = new TimeTablesPage();
                    Page = TimeTablesPageProp;
                }
            }
        }
        #endregion
    }
}
