using ProgramManager.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ProgramManager.SystemUtility
{
    /// <summary>
    /// ViewModel zarzadzajacy danymi odnosnie zainstalowanych aplikacji oraz ich zmian
    /// </summary>
    public class InstalledSoftwareViewModel : BaseViewModel
    {
        public string Search { get; set; }
        public ObservableCollection<Software> SoftwareList { get; set; }
        public ObservableCollection<KeyValuePair<Software, SoftwareChangeStatus>> ChangedSoftwareList { get; set; }
        /// <summary>
        /// Inicjalizuje instancje klasy <see cref="InstalledSoftwareViewModel"/>, tworzy referencje do <see cref="SoftwareList"/>
        /// oraz <see cref="ChangedSoftwareList"/>. Uruchamia <see cref="UpdateTask"/>
        /// </summary>
        public InstalledSoftwareViewModel() 
        {
            SoftwareList = InstalledSoftware.GetInstance().InstalledSoftwareList;
            InstalledSoftwareHandler installedSoftwareHandler = new InstalledSoftwareHandler();
            ChangedSoftwareList = installedSoftwareHandler.ChangedSoftwareDictionary;
         
            UpdateTask ut = new UpdateTask(installedSoftwareHandler);
            ut.Run(3);
        }
    }
}
