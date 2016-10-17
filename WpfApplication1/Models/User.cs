using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Models
{
    public class User
    {
        public string id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string dob { get; set; }
        public char gender { get; set; }
        
        private string key;
        public string getKey()
        {
            return key;
        }
        public void setKey(string key)
        {
            this.key = key;
        }
        
    }
    
}
