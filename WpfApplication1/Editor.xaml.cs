using Firebase.Database;
using Firebase.Database.Query;
using MahApps.Metro.Controls;
using System;
using System.Windows;
using WpfApplication1.Models;

namespace WpfApplication1
{
    
    public partial class Editor: MetroWindow
    {
        private FirebaseClient path;
        private Modules module;
        public Editor(Modules module)
        {
            this.module = module;
            path= App.root;
            InitializeComponent();
            if (module.buggy)
                buggyBox.SelectedIndex = 1;
            else
                buggyBox.SelectedIndex = 0;
            if (module.status == "New")
                statusBox.SelectedIndex = 0;
            else if (module.status == "In Progress")
                statusBox.SelectedIndex = 1;
            else if (module.status == "Resolved")
                statusBox.SelectedIndex = 2;
            else if (module.status == "Closed")
                statusBox.SelectedIndex = 3;
            descriptionBox.Text= module.description;
        }
        
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void pushModule()
        {
            this.module.status = statusBox.SelectionBoxItem.ToString();
            this.module.buggy = Convert.ToBoolean(buggyBox.SelectionBoxItem.ToString());
            this.module.description = descriptionBox.Text;
            await path.Child("modules").Child(module.getKey()).PutAsync(module);
        }
        private async void pushFunction()
        {
            Functions f = new Functions()
            {
                name=NameBox.Text,
                parameters=ParamBox.Text,
                returntype=ReturnTypeBox.Text,
                description=AddDescriptionBox.Text,
                module=module.getKey()
            };
            await path.Child("functions").PostAsync(f);
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            if (TabController.SelectedIndex == 0)
            {
                Console.WriteLine("UPdating Module");
                pushModule();
            }

            else
            {
                Console.WriteLine("Pushing Function");
                pushFunction();
                
            }
                
            
            this.Close();
        }
    }
}
