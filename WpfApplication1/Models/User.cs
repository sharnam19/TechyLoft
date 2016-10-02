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
        public IDictionary<string, string> projects { get; set; }
    }
}
