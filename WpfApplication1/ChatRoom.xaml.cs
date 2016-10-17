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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for ChatRoom.xaml
    /// </summary>
    /// 
    public partial class ChatRoom : TabItem
    {
        
        private ChildQuery path;
        private IDisposable observable;
        public ObservableCollection<Message> messages { get; private set; }
        public ChatRoom(string header)
        {
            InitializeComponent();
            path = App.root.Child("chatrooms").Child(header);
            messages = new AsyncObservableCollection<Message>();
            Header = header;
            getMessage(header);
        }

        ~ChatRoom()
        {
            observable.Dispose();
            path.Dispose();
        }

        private void getMessage(string header)
        {
            observable = path.OrderByKey().LimitToLast(50).AsObservable<Message>().Subscribe(d =>
            {
                Dispatcher.BeginInvoke(new Action(delegate
                {
                    Console.WriteLine("Ye Message aya hai bhai log: "+d.Object.content);
                    messages.Add(d.Object);
                }));
            });
        }

        private async void send(Message message)
        {
            await path.PostAsync<Message>(message);
        }

        private void Send(object sender, RoutedEventArgs e)
        {
            if (ContentBox.Text != "")
            {
                var message = new Message()
                {
                    content = ContentBox.Text,
                    author = ((App)Application.Current).user.id,
                    timestamp = DateTime.Now.ToString("HH:mm")
                };
                send(message);
                ContentBox.Text = "";       
            }
            
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer sv = sender as ScrollViewer;
            //if (e.ExtentHeightChange != 0 && Math.Abs(sv.VerticalOffset - sv.ScrollableHeight) < 20)
            if (sv.ScrollableHeight - sv.VerticalOffset < 20)
            {
                sv.ScrollToEnd();
            }
        }
    }
}
