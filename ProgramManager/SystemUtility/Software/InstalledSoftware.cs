using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ProgramManager.SystemUtility
{
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

        public ObservableCollection<Software> InstalledSoftwareList { get => _installedSoftwareList; }

        public int InstalledSoftwareCount
        {
            get => _installedSoftwareList.Count;
        }

        private InstalledSoftware() {
            _installedSoftwareList = new ObservableCollection<Software>();
            GetInstalledSoftwareList();
        }

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

        public void UpdateInstalledSoftwareList()
        {
            GetInstalledSoftwareList();
        }
    }
}
