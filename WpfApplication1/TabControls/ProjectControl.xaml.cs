using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication1.Models;

namespace WpfApplication1.TabControls
{
    /// <summary>
    /// Interaction logic for ProjectControl.xaml
    /// </summary>
    public partial class ProjectControl : UserControl
    {
        
        public static ObservableCollection<Projects>[] collections { get; set; }
        
        private ChildQuery path;
        private IDisposable observable;

        private HashSet<string> projectsSet;
        public ProjectControl()
        {
            path = App.root.Child("projects");
            InitializeComponent();
            collections = new AsyncObservableCollection<Projects>[5];
            for (int i = 0; i < 5; i++)
            {
                collections[i] = new AsyncObservableCollection<Projects>();
            }
            projectsSet = new HashSet<string>();
        }
        ~ProjectControl()
        {
            collections = null;
            path.Dispose();
        }

        public void getAll()
        {
            observable=path.AsObservable<Projects>().Subscribe(d =>
            {
                if (projectsSet.Contains(d.Object.name))
                {
                    if (d.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate)
                    {
                        if (!collections[0].Contains(d.Object))
                        {
                            collections[0].Add(d.Object);
                        }
                    }
                    else
                    {
                        collections[d.Object.state].Remove(d.Object);
                        collections[0].Remove(d.Object);
                    }
                }
            });
        }
        public void insertOrUpdate(string project)
        {
            if (!projectsSet.Contains(project))
            {
                projectsSet.Add(project);
            }
        }
        public void remove(string project)
        {
            projectsSet.Remove(project);
        }
    }
}
