using System.ComponentModel;
using System.Windows;
using WpfApplication1.TabControls;

namespace WpfApplication1
{
    public class Modules:INotifyPropertyChanged
    {
        private string nameR;
        private string projectR;
        private string statusR;
        private string user_idR;
        private string typeR;
        private bool buggyR;
        private string descriptionR;
        private string key;
        public string getKey()
        {
            return key;
        }
        public void setKey(string key)
        {
            this.key = key;
        }

        public string name
        {
            get
            {
                return nameR;
            }

            set
            {
                nameR = value;
                OnPropertyChanged("count");
            }
        }
        public string project
        {
            get
            {
                return projectR;
            }

            set
            {
                projectR = value;
                OnPropertyChanged("project");
            }
        }
        public string status
        {
            get
            {
                return statusR;
            }

            set
            {
                KanbanActivityControls.collections[statusToIntTranslator()].Remove(this);
                statusR = value;
                KanbanActivityControls.collections[statusToIntTranslator()].Add(this);
                //OnPropertyChanged("status");
            }
        }

        public string user_id
        {
            get
            {
                return user_idR;
            }

            set
            {    
                user_idR = value;
                OnPropertyChanged("user_id");
            }
        }
        public string description
        {
            get
            {
                return descriptionR;
            }

            set
            {
                descriptionR= value;
                OnPropertyChanged("description");
            }
        }

        public string type
        {
            get
            {
                return typeR;
            }

            set
            {
                typeR = value;
                OnPropertyChanged("type");
            }
        }
        public bool buggy
        {
            get
            {
                return buggyR;
            }

            set
            {
                buggyR = value;
                OnPropertyChanged("buggy");
            }
        }

        public int statusToIntTranslator()
        {
            if (statusR == "Resolved")
            {
                return 3;
            }
            else if (statusR == "Closed")
            {
                return 4;
            }
            else if (statusR == "New")
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
