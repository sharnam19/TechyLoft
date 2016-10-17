using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using WpfApplication1.Models;
using WpfApplication1.TabControls;
using System.Windows.Navigation;
using Firebase.Database.Query;
using Firebase.Database;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for LoggedInWindow.xaml
    /// </summary>
    public partial class LoggedInWindow : MetroWindow
    {
        private User user;
        private ActiveTab activeTab;
        private FirebaseClient path;
        private AnalyticsControls analyticsControls;
        private DashboardControl dashboardControls;
        private GroupThreadsControls groupThreadsControls;
        private KanbanActivityControls kanbanActivityControls;
        private ProjectControl projectControls;
        private TasksControls tasksControls;
        private UsersControl usersControls;

        private IDisposable projectsObservable;

        enum ActiveTab
        {
            Dashboard,
            Projects,
            Tasks,
            Users,
            KanbanActivity,
            GroupThread,
            Analytics
        };

        public LoggedInWindow()
        {
            path = App.root;
            user = ((App)Application.Current).user;
            InitializeComponent();
            this.Title = "Welcome " + user.name + " to TechyLoft!";
            this.SizeChanged += MainWindow_SizeChanged;
            dashboardControls = new DashboardControl();
            groupThreadsControls= new GroupThreadsControls();
            projectControls = new ProjectControl();
            this.ContentController.Content = dashboardControls;
            this.activeTab = ActiveTab.Dashboard;
            this.Background= new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/WpfApplication1;component/Images/icon.jpg")));
            getProjects();
        }
        ~LoggedInWindow()
        {
            projectsObservable.Dispose();
        }

        void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void New_Button(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private ActiveTab get_tab(Button button)
        {
            Button[] buttons = { DashboardButton, ProjectsButton, TasksButton, UsersButton, KanbanActivityButton, GroupThreadsButton, AnalyticsButton };
            int i = 0;
            while(i<buttons.Length && !buttons[i].Equals(button))
                i++;
            return (ActiveTab)i;
        }

        private void TabButton_Click(object sender, RoutedEventArgs e)
        {
            ActiveTab tab = get_tab((Button)sender);
            if (activeTab != tab)
            {
                this.activeTab = tab;
                switch (tab)
                {
                    case ActiveTab.Dashboard:
                        if (dashboardControls == null)
                            dashboardControls = new DashboardControl();
                        this.ContentController.Content = dashboardControls;
                        break;
                    case ActiveTab.Projects:
                        if (projectControls == null)
                            projectControls = new ProjectControl();
                        this.ContentController.Content = projectControls;
                        break;
                    case ActiveTab.Tasks:
                        if (tasksControls == null)
                            tasksControls = new TasksControls();
                        this.ContentController.Content = tasksControls;
                        break;
                    case ActiveTab.Users:
                        if (usersControls== null)
                            usersControls = new UsersControl();
                        this.ContentController.Content = usersControls;
                        break;
                    case ActiveTab.KanbanActivity:
                        if (kanbanActivityControls== null)
                            kanbanActivityControls= new KanbanActivityControls();
                        this.ContentController.Content = kanbanActivityControls;
                        break;
                    case ActiveTab.GroupThread:
                        if (groupThreadsControls== null)
                            groupThreadsControls = new GroupThreadsControls();
                        this.ContentController.Content = groupThreadsControls;
                        break;
                    case ActiveTab.Analytics:
                        if (analyticsControls== null)
                            analyticsControls = new AnalyticsControls();
                        this.ContentController.Content = analyticsControls ;
                        break;
                }
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).user = null;
            (new LoginWindow()).Show();
            this.Close();
        }

        private void Create_Click(object sender, MouseButtonEventArgs e)
        {
            if(((TextBlock)sender).Text.Equals("Project"))
            {
                Console.WriteLine("Project Created"); 
            }else if(((TextBlock)sender).Text.Equals("User")){
                Console.WriteLine("User Created");
            }
        }

        public void getProjects()
        {
            projectsObservable = App.root.Child("userprojects").Child(((App)Application.Current).getUserKey()).AsObservable<string>().Subscribe(d => {
                if (d.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate)
                {
                    groupThreadsControls.insertOrUpdate(d.Object);
                    projectControls.insertOrUpdate(d.Object);
                }
                else
                {
                    groupThreadsControls.delete(d.Object);
                }
            });
        }
    }
}
