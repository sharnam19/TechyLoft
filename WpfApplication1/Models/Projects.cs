using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.TabControls;

namespace WpfApplication1.Models
{
    public class Projects: INotifyPropertyChanged
    {
        private string githubR;
        private string nameR;
        private int countR;
        private int stateR;
        
        public event PropertyChangedEventHandler PropertyChanged;
        public int state {
            get {
                return stateR;
            }

            set {

                if (stateR != value)
                {
                    ProjectControl.collections[stateR].Remove(this);
                    ProjectControl.collections[value].Add(this);;
                }
                stateR = value;
                OnPropertyChanged("state");
            }
        }
        public int count
        {
            get
            {
                return countR;
            }

            set
            {
                countR = value;
                OnPropertyChanged("count");
            }
        }
        public string github
        {
            get
            {
                return githubR;
            }

            set
            {
                githubR= value;
                OnPropertyChanged("github");
            }
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
                OnPropertyChanged("name");
            }
        }

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
