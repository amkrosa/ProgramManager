using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace ProgramManager.SystemUtility
{
    public class InstalledSoftwareHandler
    {
        private ObservableCollection<Software> _oldInstalledSoftWareList;
        private InstalledSoftware _installedSoftware;
        private Dictionary<Software, SoftwareChangeStatus> _changedSoftwareDictionary;

        public InstalledSoftwareHandler()
        {
            _installedSoftware = InstalledSoftware.GetInstance();
            _changedSoftwareDictionary = new Dictionary<Software, SoftwareChangeStatus>();
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

        private void CompareCollections()
        {
            List<Software> oldList = new List<Software>(_oldInstalledSoftWareList);
            List<Software> newList = new List<Software>(_installedSoftware.InstalledSoftwareList);

            var removed = oldList.Except(newList).ToList();
            var added = newList.Except(oldList).ToList();

            if (removed != null)
            {
                foreach(Software s in removed)
                {
                    System.Diagnostics.Trace.WriteLine("removed => "+s.Name);
                }
            }
            if (added != null)
            {
                foreach (Software s in added)
                {
                    System.Diagnostics.Trace.WriteLine("added => " + s.Name);
                }
            }
        }

        public void Update()
        {
            _oldInstalledSoftWareList = new ObservableCollection<Software>(_installedSoftware.InstalledSoftwareList);
            _installedSoftware.UpdateInstalledSoftwareList();
            CompareCollections();
            System.Diagnostics.Trace.WriteLine("Update executed. New list count: " 
                + _installedSoftware.InstalledSoftwareCount
                +", old list count: "+_oldInstalledSoftWareList.Count);
            System.Diagnostics.Trace.WriteLine("Changed done:");
            /*foreach (KeyValuePair<Software, SoftwareChangeStatus> entry in _changedSoftwareDictionary)
            {
                Software key = entry.Key;
                SoftwareChangeStatus value = entry.Value;
                System.Diagnostics.Trace.WriteLine(key.Name + " => " + value);
            }*/
            _changedSoftwareDictionary.Clear();
        }

    }
}
