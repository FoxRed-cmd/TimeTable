using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using TimeTable.Pages;

namespace TimeTable
{
    public class ComboData
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
    public class MainDataViewModel : ViewModel
    {
        private Page page;
        private List<User> users;
        private List<Student> students;
        private List<Group> groups;
        private List<ComboData> comboDatasGroup = new List<ComboData>();
        private List<ComboData> comboDatasSubject = new List<ComboData>();
        private string searchPatternForUser;
        private string searchPatternForStudent;
        private string searchPatternForGroup;

        #region Свойства
        public Page Page
        {
            get { return page; }
            set { Set(ref page, value); }
        }
        public StudentsPage StudentsPageProp { get; set; }
        public GroupsPage GroupsPageProp { get; set; }
        public TimeTablesPage TimeTablesPageProp { get; set; }
        public SubjectsPage SubjectsPageProp { get; set; }
        public UsersPage UsersPageProp { get; set; }
        public List<ComboData> ComboDataGroup { get; set; }
        public List<ComboData> ComboDataSubject { get; set; }
        public List<Student> Students
        { 
            get => students; 
            set => Set(ref students, value); 
        }
        public List<Group> Groups
        { 
            get => groups; 
            set => Set(ref groups, value); 
        }
        public List<TimeTableModel> TimeTableModels { get; set; }
        public List<SubjectModel> SubjectModels { get; set; }
        public List<User> Users
        {
            get => users;
            set => Set(ref users, value);
        }
        public string SearchPatternForUser
        {
            get => searchPatternForUser;
            set
            {
                Set(ref searchPatternForUser, value);
                Users = User.GetAllDataFromTable().ToList().FindAll(e => e.Login.ToLower().Contains(SearchPatternForUser.ToLower()) 
                                                            || e.Password.ToLower().Contains(SearchPatternForUser.ToLower()) 
                                                            || e.Status.ToLower().Contains(SearchPatternForUser.ToLower()));
            }
        }
        public string SearchPatternForStudent
        {
            get => searchPatternForStudent;
            set
            {
                Set(ref searchPatternForStudent, value);
                Students = Student.GetAllDataFromTable().ToList().FindAll(s => s.Login.ToLower().Contains(searchPatternForStudent.ToLower())
                                                                  || s.Name.ToLower().Contains(searchPatternForStudent.ToLower())
                                                                  || s.Group.ToLower().Contains(searchPatternForStudent.ToLower())
                                                                  || s.Phone.ToLower().Contains(searchPatternForStudent.ToLower())
                                                                  || s.Email.ToLower().Contains(searchPatternForStudent.ToLower()));
            }
        }
        public string SearchPatternForGroup
        {
            get => searchPatternForGroup;
            set 
            { 
                Set(ref searchPatternForGroup, value);
                Groups = Group.GetAllDataFromTable().ToList().FindAll(g => g.Name.ToLower().Contains(searchPatternForGroup.ToLower())
                                                              || g.Description.ToLower().Contains(searchPatternForGroup.ToLower())
                                                              || g.FormOfStudy.ToLower().Contains(searchPatternForGroup.ToLower())
                                                              || g.TrainingPeriod.ToLower().Contains(searchPatternForGroup.ToLower()));
            }
        }
        #endregion

        public MainDataViewModel()
        {
            Page = new WelcomePage();
            Students = Student.GetAllDataFromTable().ToList();
            Groups = Group.GetAllDataFromTable().ToList();
            TimeTableModels = TimeTableModel.GetAllDataFromTable().ToList();
            SubjectModels = SubjectModel.GetAllDataFromTable().ToList();
            Users = User.GetAllDataFromTable().ToList();
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
            OpenSubjectsPageCommand = new LambdaCommand(OnOpenSubjectsPageCommandExecuted, CanOpenSubjectsPageCommandExecute);
            OpenUsersPageCommand = new LambdaCommand(OnOpenUsersPageCommandExecuted, CanOpenUsersPageCommandExecute);

            RefreshUserDataCommand = new LambdaCommand(OnRefreshUserDataCommandExecuted, CanRefreshUserDataCommandExecute);
            RefreshStudentDataCommand = new LambdaCommand(OnRefreshStudentDataCommandExecuted, CanRefreshStudentDataCommandExecute);
            RefreshGroupDataCommand = new LambdaCommand(OnRefreshGroupDataCommandExecuted, CanRefreshGroupDataCommandExecute);
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

        public ICommand OpenSubjectsPageCommand { get; }
        private bool CanOpenSubjectsPageCommandExecute(object p)
        {
            return true;
        }
        private void OnOpenSubjectsPageCommandExecuted(object p)
        {
            if (Page is not SubjectsPage)
            {
                if (SubjectsPageProp != null)
                    Page = SubjectsPageProp;
                else
                {
                    SubjectsPageProp = new SubjectsPage();
                    Page = SubjectsPageProp;
                }
            }
        }

        public ICommand OpenUsersPageCommand { get; }
        private bool CanOpenUsersPageCommandExecute(object p)
        {
            return true;
        }
        private void OnOpenUsersPageCommandExecuted(object p)
        {
            if (Page is not UsersPage)
            {
                if (UsersPageProp != null)
                    Page = UsersPageProp;
                   
                else
                {
                    UsersPageProp = new UsersPage();
                    Page = UsersPageProp;
                }
            }
        }

        public ICommand RefreshUserDataCommand { get; }
        private bool CanRefreshUserDataCommandExecute(object p)
        {
            return true;
        }
        private void OnRefreshUserDataCommandExecuted(object p)
        {
            Users = User.GetAllDataFromTable().ToList();
        }

        public ICommand RefreshStudentDataCommand { get; }
        private bool CanRefreshStudentDataCommandExecute(object p)
        {
            return true;
        }
        private void OnRefreshStudentDataCommandExecuted(object p)
        {
            Students = Student.GetAllDataFromTable().ToList();
        }

        public ICommand RefreshGroupDataCommand { get; }
        private bool CanRefreshGroupDataCommandExecute(object p)
        {
            return true;
        }
        private void OnRefreshGroupDataCommandExecuted(object p)
        {
            Groups = Group.GetAllDataFromTable().ToList();
        }
        #endregion
    }
}
