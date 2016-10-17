using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using WpfApplication1.Models;

namespace WpfApplication1.TabControls
{
    /// <summary>
    /// Interaction logic for GroupThreadsControls.xaml
    /// </summary>
    public partial class GroupThreadsControls : UserControl
    {
        private Dictionary<string,ChatRoom> projectMap;
        
        public GroupThreadsControls()
        {
            InitializeComponent();
            projectMap = new Dictionary<string,ChatRoom>();
        }
        
        public void insertOrUpdate(string header)
        {
            Console.WriteLine("GRoup THreads: " + header + " ChatRoom Creation");
            Dispatcher.BeginInvoke(new Action(delegate
            {
                ChatRoom c;
                if (!projectMap.TryGetValue(header, out c))
                {
                    c = new ChatRoom(header);
                    TabController.Items.Add(c);
                    projectMap.Add(header, c);
                    if (projectMap.Values.Count == 1)
                        TabController.SelectedItem = 0;
                }
            }));
        }
        public void delete(string header)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                ChatRoom c;
                if (projectMap.TryGetValue(header, out c))
                {
                    projectMap.Remove(header);
                    TabController.Items.Remove(c);
                }
            }));
        }

    }
}
