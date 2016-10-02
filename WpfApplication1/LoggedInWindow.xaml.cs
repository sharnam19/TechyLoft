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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for LoggedInWindow.xaml
    /// </summary>
    public partial class LoggedInWindow : MetroWindow
    {
        private User user;
        private ActiveTab activeTab;

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

        public LoggedInWindow(User user)
        {
            this.user = user;
            InitializeComponent();
            this.Title = "Welcome " + user.name + " to TechyLoft!";
            this.SizeChanged += MainWindow_SizeChanged;
            this.ContentController.Content = new DashboardControl();
            this.activeTab = ActiveTab.Dashboard;
            this.Background= new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/WpfApplication1;component/Images/icon.jpg")));
        }

        void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void TabButton_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource == DashboardButton)
            {
                if (activeTab != ActiveTab.Dashboard)
                {
                    this.ContentController.Content = new DashboardControl();
                    this.activeTab = ActiveTab.Dashboard;
                }
            }else if (e.OriginalSource == ProjectsButton)
            {
                if (activeTab != ActiveTab.Projects)
                {
                    this.ContentController.Content = new ProjectControl();
                    this.activeTab = ActiveTab.Projects;
                }
            }else if (e.OriginalSource == TasksButton)
            {
                if (activeTab != ActiveTab.Tasks)
                {
                    this.ContentController.Content = new TasksControls();
                    this.activeTab = ActiveTab.Tasks;
                }
            }else if (e.OriginalSource == UsersButton)
            {
                if (activeTab != ActiveTab.Users)
                {
                    this.ContentController.Content = new UsersControl();
                    this.activeTab = ActiveTab.Users;
                }
            }else if (e.OriginalSource == KanbanActivityButton)
            {
                if (activeTab != ActiveTab.KanbanActivity)
                {
                    this.ContentController.Content = new KanbanActivityControls();
                    this.activeTab = ActiveTab.KanbanActivity;
                }
            }
            else if (e.OriginalSource == GroupThreadsButton)
            {
                if (activeTab != ActiveTab.GroupThread)
                {
                    this.ContentController.Content = new GroupThreadsControls();
                    this.activeTab = ActiveTab.GroupThread;
                }
            }
            else if (e.OriginalSource == this.AnalyticsButton)
            {
                if (activeTab != ActiveTab.Analytics)
                {
                    this.ContentController.Content = new AnalyticsControls();
                    this.activeTab = ActiveTab.Analytics;
                }
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
