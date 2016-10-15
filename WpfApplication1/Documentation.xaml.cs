using Firebase.Database.Query;
using MahApps.Metro.Controls;
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
using System.Windows.Shapes;
using WpfApplication1.Models;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Documentation.xaml
    /// </summary>
    public partial class Documentation : MetroWindow
    {
        public ObservableCollection<Functions> functions { get; private set; }
        public Dictionary<string,Functions> functionsHashMap { get; private set; }
        private IDisposable observable;
        private ChildQuery path;

        public Documentation(Modules module)
        {
            path = App.root.Child("functions");
            functions = new AsyncObservableCollection<Functions>();
            functionsHashMap = new Dictionary<string, Functions>();
            InitializeComponent();
            getData(module.getKey());
            //getData("module");
        }
        private async void getData(string modulekey){

            observable = path.OrderBy("module").EqualTo(modulekey).AsObservable<Functions>().Subscribe(d => {
                if (d.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate)
                {
                    Console.WriteLine("Here");
                    d.Object.setKey(d.Key);
                    Dispatcher.BeginInvoke(new Action(delegate
                    {
                        Functions val1;
                        if (functionsHashMap.TryGetValue(d.Key,out val1))
                        {
                            functions.Remove(val1);
                            functionsHashMap.Remove(d.Key);
                            if (d.Object.module.Equals(modulekey))
                            {
                                functionsHashMap.Add(d.Key, d.Object);
                                functions.Add(d.Object);
                            }
                        }else
                        {
                            functionsHashMap.Add(d.Key, d.Object);
                            functions.Add(d.Object);
                        }
                        
                    }));
                    
                }else if (d.EventType == Firebase.Database.Streaming.FirebaseEventType.Delete)
                {
                    Console.WriteLine("removed");
                    Dispatcher.BeginInvoke(new Action(delegate
                    {
                        Functions val2;
                        if (functionsHashMap.TryGetValue(d.Key,out val2))
                        {
                            functions.Remove(val2);
                            functionsHashMap.Remove(d.Key);
                        }
                    }));
                }
            });

        }

        ~Documentation()
        {
            observable.Dispose();
        }

    }
}
