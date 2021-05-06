using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramManager.SystemUtility
{
    class Software
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public bool isUpdateNeeded { get; set; }
        public Software(string name, string version)
        {
            this.Name = name;
            this.Version = version;
            isUpdateNeeded = false;
        }
    }
}
