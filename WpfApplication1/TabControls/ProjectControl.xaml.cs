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

        /*public ObservableCollection<Projects> all { get; private set; }
        public ObservableCollection<Projects> starred { get; private set; }
        public ObservableCollection<Projects> onHold { get; private set; }
        public ObservableCollection<Projects> stack { get; private set; }
        public ObservableCollection<Projects> completed { get; private set; }
        */

        public ObservableCollection<Projects>[] collections { get; set; }
        private Dictionary<string,Projects>[] dictionaries { get; set; }

        /*private Dictionary<string, Projects> allMap;
        private Dictionary<string, Projects> starredMap;
        private Dictionary<string, Projects> onHoldMap;
        private Dictionary<string, Projects> stackMap;
        private Dictionary<string, Projects> completedMap;
        */
        private ChildQuery path;
        private Dictionary<string,IDisposable> observables;

        private HashSet<string> projectsSet;
        public ProjectControl()
        {
            path = App.root.Child("projects");
            InitializeComponent();
            collections = new ObservableCollection<Projects>[5];
            dictionaries= new Dictionary<string,Projects>[5];
            observables = new Dictionary<string, IDisposable>();
            for (int i = 0; i < 5; i++)
            {
                collections[i] = new AsyncObservableCollection<Projects>();
                dictionaries[i] = new Dictionary<string, Projects>();
            }
            
            /*
            all = new AsyncObservableCollection<Projects>();
            starred= new AsyncObservableCollection<Projects>();
            onHold = new AsyncObservableCollection<Projects>();
            stack= new AsyncObservableCollection<Projects>();
            completed = new AsyncObservableCollection<Projects>();

            allMap = new Dictionary<string, Projects>();
            starredMap = new Dictionary<string, Projects>();
            onHoldMap = new Dictionary<string, Projects>();
            stackMap = new Dictionary<string, Projects>();
            completedMap = new Dictionary<string, Projects>();
            */
        }
        ~ProjectControl()
        {
            path.Dispose();
        }

        public void insertOrUpdate(string project)
        {
            Console.WriteLine(project+" AYA!");
            observables.Add(project,path.OrderBy("name").EqualTo(project).AsObservable<Projects>().Subscribe(d =>
            {
                ObservableCollection<Projects> referCollection = collections[d.Object.state];
                Dictionary<string,Projects> referDict = dictionaries[d.Object.state];

                if (d.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate)
                {
                    Projects c;
                    Console.WriteLine("FOr Project: " + project + " :" + d.Object.github + " :" + d.Object.state);
                    Dispatcher.BeginInvoke(new Action(delegate
                    {
                        if (dictionaries[0].TryGetValue(project,out c))
                        {
                            
                                Console.WriteLine("Idhar Hoon Mein");
                            
                                collections[c.state].Remove(c);
                                dictionaries[c.state].Remove(project);

                                collections[0].Remove(c);
                                dictionaries[0].Remove(project);

                                collections[0].Add(d.Object);
                                dictionaries[0].Add(project, d.Object);

                                collections[d.Object.state].Add(d.Object);
                                dictionaries[d.Object.state].Add(project,d.Object);
                            
                    }else
                    {
                            Console.WriteLine("Idhar Hoon Mein3");
                            collections[d.Object.state].Add(d.Object);
                            dictionaries[d.Object.state].Add(project, d.Object);
                            collections[0].Add(d.Object);
                            dictionaries[0].Add(project, d.Object);
                    }
                    }));
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(delegate
                    {
                        Projects c;
                    if(dictionaries[0].TryGetValue(project,out c))
                    {

                            collections[c.state].Remove(c);
                            dictionaries[c.state].Remove(project);
                            collections[0].Remove(c);
                            dictionaries[0].Remove(project);
                        
                    }
                    IDisposable obs;
                    if(observables.TryGetValue(project, out obs)){
                        obs.Dispose();
                    }
                }));
        }
            }));
        }
    }
}
