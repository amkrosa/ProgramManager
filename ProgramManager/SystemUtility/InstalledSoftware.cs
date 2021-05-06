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

        private Dictionary<string, string> _installedSoftwareList;

        public Dictionary<string, string> InstalledSoftwareList { get; private set;}

        private InstalledSoftware() { }

        private Dictionary<string, string> GetInstalledSoftwareDictionary()
        {
            string displayName;
            string displayVersion;
            Dictionary<string, string> installedSoftwareDictionary = new Dictionary<string, string>();

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false))
            {
                foreach (String keyName in key.GetSubKeyNames())
                {
                    RegistryKey subkey = key.OpenSubKey(keyName);
                    displayName = subkey.GetValue("DisplayName") as string;
                    displayVersion = subkey.GetValue("DisplayVersion") as string;
                    displayVersion = string.IsNullOrEmpty(displayVersion) ? "unknown" : displayVersion;

                    if (string.IsNullOrEmpty(displayName) || installedSoftwareDictionary.ContainsKey(displayName.ToLower()))
                        continue;

                    installedSoftwareDictionary.Add(displayName.ToLower(), displayVersion.ToLower());
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

                    if (string.IsNullOrEmpty(displayName) || installedSoftwareDictionary.ContainsKey(displayName.ToLower()))
                        continue;

                    installedSoftwareDictionary.Add(displayName.ToLower(), displayVersion.ToLower());
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

                    if (string.IsNullOrEmpty(displayName) || installedSoftwareDictionary.ContainsKey(displayName.ToLower()))
                        continue;

                    installedSoftwareDictionary.Add(displayName.ToLower(), displayVersion.ToLower());
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


                    if (string.IsNullOrEmpty(displayName) || installedSoftwareDictionary.ContainsKey(displayName.ToLower()))
                        continue;

                    installedSoftwareDictionary.Add(displayName.ToLower(), displayVersion.ToLower());
                }
            }

            System.Diagnostics.Trace.WriteLine("Updating installedSoftwareDictionary is done...");

            return installedSoftwareDictionary;
        }

        public void Update()
        {
            InstalledSoftware installedSoftware = InstalledSoftware.GetInstance();
            InstalledSoftwareList = GetInstalledSoftwareDictionary();
        }
    }
}
