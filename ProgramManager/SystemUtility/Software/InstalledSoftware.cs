using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ProgramManager.SystemUtility
{
    /// <summary>
    /// Singleton przechowywujacy oraz aktualizujacy liste zawierajaca aktualnie zainstalowane programy. 
    /// </summary>
    class InstalledSoftware
    {
        private static InstalledSoftware _instance;
        public static InstalledSoftware GetInstance()
        {
            if (_instance == null)
            {
                _instance = new InstalledSoftware();
            }
            return _instance;
        }

        private ObservableCollection<Software> _installedSoftwareList;
        /// <summary>
        /// Lista aktualnie zainstalowanych programow
        /// </summary>
        public ObservableCollection<Software> InstalledSoftwareList { get => _installedSoftwareList; }

        private InstalledSoftware()
        {
            _installedSoftwareList = new ObservableCollection<Software>();
            GetInstalledSoftwareList();
        }

        /// <summary>
        /// Dodanie programow do <see cref="InstalledSoftware.InstalledSoftwareList"/> na bazie podanego <see cref="RegistryKey"/>.
        /// </summary>
        /// <param name="registryKey">Klucz rejestru</param>
        /// <param name="isBaseKey"></param>
        private void AddSoftwareByRegistryKey(RegistryKey registryKey, bool isBaseKey)
        {
            string displayName;
            string displayVersion;
            RegistryKey subKey;
            using (registryKey)
            {
                subKey = isBaseKey ? registryKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false) :
                    registryKey;

                foreach (String keyName in subKey.GetSubKeyNames())
                {
                    RegistryKey subkey = subKey.OpenSubKey(keyName);
                    displayName = subkey.GetValue("DisplayName") as string;
                    displayVersion = subkey.GetValue("DisplayVersion") as string;
                    displayVersion = string.IsNullOrEmpty(displayVersion) ? "unknown" : displayVersion;

                    if (string.IsNullOrEmpty(displayName))
                        continue;

                    Software software = new Software(displayName.ToLower(), displayVersion.ToLower());

                    if (_installedSoftwareList.Contains(software))
                        continue;

                    _installedSoftwareList.Add(software);
                }
            }
        }

        /// <summary>
        /// Wyczyszczenie <see cref="InstalledSoftwareList"/>, a nastepnie dodanie programow z 4 roznych lokalizacji w rejestrze.
        /// </summary>
        private void GetInstalledSoftwareList()
        {
            _installedSoftwareList.Clear();

            RegistryKey subKeyUninstall = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            RegistryKey baseKeyUninstall64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey baseKeyUninstall32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            RegistryKey subKeyWow6432Node = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");

            AddSoftwareByRegistryKey(subKeyUninstall, false);
            AddSoftwareByRegistryKey(baseKeyUninstall64, true);
            AddSoftwareByRegistryKey(baseKeyUninstall32, true);
            AddSoftwareByRegistryKey(subKeyWow6432Node, false);
        }

        /// <summary>
        /// Publiczna metoda do bezposredniej aktualizacji <see cref="InstalledSoftwareList"/>
        /// </summary>
        public void UpdateInstalledSoftwareList()
        {
            GetInstalledSoftwareList();
        }
    }
}
