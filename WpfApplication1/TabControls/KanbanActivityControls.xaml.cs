using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApplication1.TabControls
{
    public partial class KanbanActivityControls : UserControl
    {
        
        public static AsyncObservableCollection<Modules>[] collections { get; set; }
        
        private IDisposable observable;
        private ChildQuery path;
        
        public KanbanActivityControls()
        {
            collections = new AsyncObservableCollection<Modules>[5];
            for(int i = 0; i < 5; i++)
            {
                collections[i] = new AsyncObservableCollection<Modules>();
            }

            InitializeComponent();
            path = App.root.Child("modules");
                        
            getData();
        }
        /*
        private void remove_AddItem(Dictionary<string,Modules> outDict,ObservableCollection<Modules> outClltn, Dictionary<string, Modules> inDict, ObservableCollection<Modules> inClltn, Modules outOb,Modules inOb,string key)
        {
            Console.WriteLine("Changed " + key);
            inOb.setKey(key);
            outDict.Remove(key);
            inDict.Add(key, inOb);

            Dispatcher.BeginInvoke(new Action(delegate
            {
                // Do your work
                outClltn.Remove(outOb);
                inClltn.Add(inOb);
                
            }));
        }
        private void addItem(Dictionary<string,Modules> dict,ObservableCollection<Modules> clltn,Modules inOb,string key)
        {

            Console.WriteLine("Added " + key);
            inOb.setKey(key);
            dict.Add(key, inOb);
            Dispatcher.BeginInvoke(new Action(delegate
            {
                // Do your work
                clltn.Add(inOb);
            }));
        }
        private void removeItem(Dictionary<string, Modules> dict, ObservableCollection<Modules> clltn, Modules inOb, string key)
        {
            dict.Remove(key);
            clltn.Remove(inOb);
        }
        */
        private delegate void Update();

        private void getData()
        {
            observable = path.OrderBy("user_id").EqualTo(((App)Application.Current).getUserId()).AsObservable<Modules>()
                        .Subscribe(d => {

                        if (d.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate)
                        {
                                if (!collections[0].Contains(d.Object))
                                {
                                    collections[0].Add(d.Object);
                                }
                           /* Modules val;
                            Dictionary<string,Modules> referDict= null;
                            ObservableCollection<Modules> referCollection = null;
                                if (d.Object.status == "Closed")
                                {
                                    referDict = closedHashMap;
                                    referCollection = closed;
                                }else if (d.Object.status == "Resolved")
                                {
                                    referDict = resolvedHashMap;
                                    referCollection = resolved;
                                }
                                else if (d.Object.status == "New")
                                {
                                    referDict = newHashMap;
                                    referCollection =newmod;
                                }
                                else if (d.Object.status == "In Progress")
                                {
                                    referDict = inprogressHashMap;
                                    referCollection = inprogress;
                                }

                                if (newHashMap.TryGetValue(d.Key, out val))
                                    remove_AddItem(newHashMap, newmod, referDict, referCollection, val, d.Object, d.Key);
                                else if (inprogressHashMap.TryGetValue(d.Key, out val))
                                    remove_AddItem(inprogressHashMap, inprogress, referDict, referCollection, val, d.Object, d.Key);
                                else if (resolvedHashMap.TryGetValue(d.Key, out val))
                                    remove_AddItem(resolvedHashMap, resolved, referDict, referCollection, val, d.Object, d.Key);
                                else if (closedHashMap.TryGetValue(d.Key, out val))
                                    remove_AddItem(closedHashMap, closed, referDict, referCollection, val, d.Object, d.Key);
                                else
                                    addItem(referDict, referCollection, d.Object, d.Key);
                                */
                            }
                            else if (d.EventType == Firebase.Database.Streaming.FirebaseEventType.Delete)
                            {
                                collections[0].Remove(d.Object);
                                collections[d.Object.statusToIntTranslator()].Remove(d.Object);
                                /*
                                Console.WriteLine("Removed "+d.Key);
                                Modules val2;
                                if (newHashMap.TryGetValue(d.Key, out val2))
                                    removeItem(newHashMap, newmod, val2, d.Key);
                                else if (inprogressHashMap.TryGetValue(d.Key, out val2))
                                    removeItem(inprogressHashMap, inprogress, val2, d.Key);
                                else if (resolvedHashMap.TryGetValue(d.Key, out val2))
                                    removeItem(resolvedHashMap, resolved, val2, d.Key);
                                else if (closedHashMap.TryGetValue(d.Key, out val2))
                                    removeItem(closedHashMap, closed, val2, d.Key);
                                */
                            }
                        });
        }

        public void ClickHandler(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Console.WriteLine("Clicked");
            Modules item = (Modules)(sender as Grid).DataContext;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                (new Documentation(item)).Show();
                //Console.WriteLine("openDocument " + item.name);
            }else if (e.RightButton == MouseButtonState.Pressed)
            {
                Editor editor = new Editor(item);
                editor.Show();
            }
        }

    }
}
