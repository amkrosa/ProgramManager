using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramManager.SystemUtility
{
    class Software
    {
        string Name { get; set; }
        string Version { get; set; }
        bool isUpdateNeeded { get; set; }
        public Software(string name, string version)
        {
            this.Name = name;
            this.Version = version;
            isUpdateNeeded = false;
        }
    }
}
