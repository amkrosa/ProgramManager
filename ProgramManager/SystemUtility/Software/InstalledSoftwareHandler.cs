using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace ProgramManager.SystemUtility
{
    public class InstalledSoftwareHandler
    {
        private ObservableCollection<Software> observableInstalledSoftware;
        private InstalledSoftware installedSoftware;

        public InstalledSoftwareHandler()
        { 

        }

        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
        }

        public void Update()
        {

        }

    }
}
