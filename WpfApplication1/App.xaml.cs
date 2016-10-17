using Firebase.Database;
using MahApps.Metro;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApplication1.Models;
namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static FirebaseClient root = new FirebaseClient("https://dazzling-heat-1022.firebaseio.com/");
        public User user;
        protected override void OnStartup(StartupEventArgs e)
        {
            // get the current app style (theme and accent) from the application
            // you can then use the current theme and custom accent instead set a new theme
            Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);
            ThemeManager.AddAccent("CustomAccent1", new Uri("pack://application:,,,/WpfApplication1;component/CustomAccents/CustomAccents1.xaml"));

            // now set the Green accent and dark theme
            ThemeManager.ChangeAppStyle(Application.Current,
                                       ThemeManager.GetAccent("CustomAccent1"),
                                        ThemeManager.GetAppTheme("BaseDark")); // or appStyle.Item1
            base.OnStartup(e);
        }
        public string getUserId()
        {
            return user.id;
        }
        public string getUserKey()
        {
            return user.getKey();
        }
    }
}
