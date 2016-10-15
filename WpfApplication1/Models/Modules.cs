namespace WpfApplication1
{
    public class Modules
    {
        public string name { get; set; }
        public string project { get; set; }
        public string status { get; set; }
        public string user_id { get; set; }
        public string type { get; set; }
        public bool buggy { get; set; }
        public string description { get; set; }
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
