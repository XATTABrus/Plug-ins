using System;
using Framework;

namespace Plugin2
{
    public class Plugin2 : IPlugin
    {
        private string _name;
        public string Name
        {
            get { return string.IsNullOrWhiteSpace(_name) ? "My default name: Plugin - 2" : _name; }

            set { _name = value; }
        }
    }
}
