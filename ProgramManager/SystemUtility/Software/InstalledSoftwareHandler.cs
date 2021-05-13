using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace ProgramManager.SystemUtility
{
    public class InstalledSoftwareHandler
    {
        private ObservableCollection<Software> _oldInstalledSoftWareList;
        private InstalledSoftware _installedSoftware;

        public InstalledSoftwareHandler()
        {
            _installedSoftware = InstalledSoftware.GetInstance();
            _oldInstalledSoftWareList = new ObservableCollection<Software>(_installedSoftware.InstalledSoftwareList);
            _installedSoftware.InstalledSoftwareList.CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            int olditems_count = 0;
            int newitems_count = 0;
            if (e.OldItems != null)
                olditems_count = e.OldItems.Count;
            if (e.NewItems != null) {
            }           
        }

        public void Update()
        {
            _oldInstalledSoftWareList = new ObservableCollection<Software>(_installedSoftware.InstalledSoftwareList);
            _installedSoftware.UpdateInstalledSoftwareList();
            System.Diagnostics.Trace.WriteLine("Update executed. New list count: " 
                + _installedSoftware.InstalledSoftwareCount
                +", old list count: "+_oldInstalledSoftWareList.Count);
        }

    }
}
