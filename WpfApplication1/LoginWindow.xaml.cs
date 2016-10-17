using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MahApps.Metro.Controls;
using System.Security.Cryptography;
using Firebase.Database;
using Firebase.Database.Query;
using WpfApplication1.Models;
using System.Windows.Media.Imaging;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow: MetroWindow
    {
        private ChildQuery path; 
        public LoginWindow()
        {
            path = App.root.Child("users");
            InitializeComponent();
            setup();
        }
        public void setup()
        {
            TextBox[] items = { this.fullnameBox, this.UsernameBox, this.usernameRegBox };
            foreach(TextBox box in items){
                box.AcceptsReturn = false;
                box.AcceptsTab = false;
            }
            this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/WpfApplication1;component/Images/icon.jpg")));
        }
        public async void getData()
        {
            //Button button= this.FindChild<Button>("button");
            /*var client = new FirebaseClient("https://dazzling-heat-1022.firebaseio.com/");
            var observable = await client.Child("modules").OrderBy("project").EqualTo("project1").AsObservable<Modules>()
                        .Subscribe(d => {
                            Console.WriteLine(d.Key);
                            d.Object.setKey(d.Key);
                               if (d.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate){
                                    this.BeginInvoke(new Action(() =>{
                                        this.button.Content = "Added Or Change";
                                        this.textBlock.Text = d.Key;
                                    }));
                                }
                            else if(d.EventType == Firebase.Database.Streaming.FirebaseEventType.Delete){
                                    this.BeginInvoke(new Action(() =>
                                    {
                                        this.button.Content= "Removed";
                                        this.textBlock.Text = d.Key;
                                    }));
                            }
                        });*/
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            bool allTrue = true;
            if (!IsValidEmail(UsernameBox.Text))
            {
                allTrue = false;
                usernameBlock.Foreground = Brushes.Red;
            }else
            {
                usernameBlock.Foreground = Brushes.White;
            }
            if (PasswordBox.Password.Length < 1)
            {
                allTrue = false;
                passwordBlock.Foreground = Brushes.Red;
            }
            else
            {
                passwordBlock.Foreground = Brushes.White;
            }
            if (allTrue) { 
                var users = await path.OrderBy("id").EqualTo(UsernameBox.Text).LimitToFirst(1).OnceAsync<User>();
                if (users.Count==0)
                {
                    Console.WriteLine("No Such User Exists");
                }else
                {
                    foreach(var user in users)
                    {
                        if (user.Object.password == PasswordBox.Password)
                        {
                            Console.WriteLine("All PAss");
                            user.Object.setKey(user.Key);
                            ((App)Application.Current).user = user.Object;
                            (new LoggedInWindow()).Show();
                            this.Close();
                        }else{
                            Console.WriteLine("Passwords Dont Match");
                        }
                    }
                }
            }else
            {
                Console.WriteLine("Some Failed2");
            }
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            bool allTrue = true;

            if (fullnameBox.Text.Length < 1)
            {
                allTrue = false;
                fullnameBlock.Foreground = Brushes.Red;
            }else
            {
                fullnameBlock.Foreground = Brushes.White;
            }
            if (!IsValidEmail(usernameRegBox.Text))
            {
                allTrue = false;
                usernameRegBlock.Foreground = Brushes.Red;
            }else
            {
                usernameRegBlock.Foreground = Brushes.White;
            }
            if (dobPicker.Text.Length < 1)
            {
                allTrue = false;
                dobBlock.Foreground = Brushes.Red;
            }else
            {
                dobBlock.Foreground = Brushes.White;
            }
            
            if (!passwordRegBox.Password.Equals(confirmPasswordBox.Password))
            {
                allTrue = false;
                passwordRegBlock.Foreground = Brushes.Orange;
                cpasswordBlock.Foreground = Brushes.Orange;
            }
            else
            {
                passwordRegBlock.Foreground = Brushes.White;
                cpasswordBlock.Foreground = Brushes.White;
                if (passwordRegBox.Password.Length < 6)
                {
                    allTrue = false;
                    passwordRegBlock.Foreground = Brushes.Red;
                }
                else
                {
                    passwordRegBlock.Foreground = Brushes.White;
                }
                if (confirmPasswordBox.Password.Length < 6)
                {
                    allTrue = false;
                    cpasswordBlock.Foreground = Brushes.Red;
                }
                else
                {
                    cpasswordBlock.Foreground = Brushes.White;
                }
                
            }
            char val='M';
            if (radioButtonF.IsChecked == true)
            {
                val = 'F';
            }            
            if (allTrue)
            {
                
                var users= await path.OrderBy("id").EqualTo(fullnameBox.Text).OnceAsync<User>();
                if (users.Count > 0)
                {
                    Console.WriteLine("User ALready Exists");
                }else{
                    var user = new User()
                    {
                        name = fullnameBox.Text,
                        id = usernameRegBox.Text,
                        dob = dobPicker.Text,
                        password = passwordRegBox.Password,
                        gender = val
                    };
                    Console.WriteLine("All PAss");
                    var ob =await path.PostAsync<User>(user);
                    ((App)Application.Current).user = user;
                    user.setKey(ob.Key);
                    (new LoggedInWindow()).Show();
                    this.Close();
                }
            }else
            {
                Console.WriteLine("Some Failed");
            }
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
