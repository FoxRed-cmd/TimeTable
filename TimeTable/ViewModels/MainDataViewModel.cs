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
        private List<ComboData> comboDatas = new List<ComboData>();
        public Page Page
        {
            get { return page; }
            set { Set(ref page, value); }
        }
        public StudentsPage StudentsPageProp { get; set; }
        public GroupsPage GroupsPageProp { get; set; }
        public List<ComboData> ComboData { get; set; }
        public List<Student> Students { get; set; }
        public List<Group> Groups { get; set; }

        public MainDataViewModel()
        {
            Page = new WelcomePage();
            Students = Student.GetAllDataFromTable().ToList();
            Groups = Group.GetAllDataFromTable().ToList();
            for (int i = 0, j = 0; i < Groups.Count; i++)
            {
                comboDatas.Add(new ComboData { Id = ++j, Value = Groups[i].Name });
            }
            ComboData = comboDatas;

            OpenStudentsPageCommand = new LambdaCommand(OnOpenStudentsPageCommandExecuted, CanOpenStudentsPageCommandExecute);
            OpenGroupsPageCommand = new LambdaCommand(OnOpenGroupsPageCommandExecuted, CanOpenGroupsPageCommandExecute);
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
        #endregion
    }
}
