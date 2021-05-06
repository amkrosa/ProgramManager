using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

        private List<Software> _installedSoftwareList;

        public List<Software> InstalledSoftwareList { get; private set;}

        private InstalledSoftware() { }

        private List<Software> GetInstalledSoftwareList()
        {
            string displayName;
            string displayVersion;
            List<Software> installedSoftwareList = new List<Software>();

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false))
            {
                foreach (String keyName in key.GetSubKeyNames())
                {
                    RegistryKey subkey = key.OpenSubKey(keyName);
                    displayName = subkey.GetValue("DisplayName") as string;
                    displayVersion = subkey.GetValue("DisplayVersion") as string;
                    displayVersion = string.IsNullOrEmpty(displayVersion) ? "unknown" : displayVersion;
                    Software software = new Software(displayName.ToLower(), displayVersion.ToLower());

                    if (string.IsNullOrEmpty(displayName) || installedSoftwareList.Contains(software))
                        continue;

                    installedSoftwareList.Add(software);
                }
            }

            using (var localMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                var key = localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false);
                foreach (String keyName in key.GetSubKeyNames())
                {
                    RegistryKey subkey = key.OpenSubKey(keyName);
                    displayName = subkey.GetValue("DisplayName") as string;
                    displayVersion = subkey.GetValue("DisplayVersion") as string;
                    displayVersion = string.IsNullOrEmpty(displayVersion) ? "unknown" : displayVersion;
                    Software software = new Software(displayName.ToLower(), displayVersion.ToLower());

                    if (string.IsNullOrEmpty(displayName) || installedSoftwareList.Contains(software))
                        continue;

                    installedSoftwareList.Add(software);
                }
            }

            using (var localMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
            {
                var key = localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false);
                foreach (String keyName in key.GetSubKeyNames())
                {
                    RegistryKey subkey = key.OpenSubKey(keyName);
                    displayName = subkey.GetValue("DisplayName") as string;
                    displayVersion = subkey.GetValue("DisplayVersion") as string;
                    displayVersion = string.IsNullOrEmpty(displayVersion) ? "unknown" : displayVersion;
                    Software software = new Software(displayName.ToLower(), displayVersion.ToLower());

                    if (string.IsNullOrEmpty(displayName) || installedSoftwareList.Contains(software))
                        continue;

                    installedSoftwareList.Add(software);
                }
            }

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall", false))
            {
                foreach (String keyName in key.GetSubKeyNames())
                {
                    RegistryKey subkey = key.OpenSubKey(keyName);
                    displayName = subkey.GetValue("DisplayName") as string;
                    displayVersion = subkey.GetValue("DisplayVersion") as string;
                    displayVersion = string.IsNullOrEmpty(displayVersion) ? "unknown" : displayVersion;
                    Software software = new Software(displayName.ToLower(), displayVersion.ToLower());

                    if (string.IsNullOrEmpty(displayName) || installedSoftwareList.Contains(software))
                        continue;

                    installedSoftwareList.Add(software);
                }
            }

            return installedSoftwareList;
        }

        public void Update()
        {
            InstalledSoftware installedSoftware = InstalledSoftware.GetInstance();
            InstalledSoftwareList = GetInstalledSoftwareList();
        }
    }
}
