using System;
using Framework;

namespace Plugin1
{
    public class Plugin1 : IPlugin
    {
        private string _name;
        public string Name
        {
            get { return string.IsNullOrWhiteSpace(_name) ? "My default name: Plugin - 1" : _name; }

            set { _name = value; }
        }
    }
}
