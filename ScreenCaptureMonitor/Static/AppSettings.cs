using System.Collections.Specialized;
using System.Collections.Generic;
using System.Drawing;

namespace ScreenCaptureMonitor.Static
{
    public static class AppSettings
    {
        [System.ComponentModel.SettingsBindable(true)]
        public static string SoundFilePath
        {
            get { return Properties.Settings.Default.SoundFilePath; }
            set
            {
                Properties.Settings.Default.SoundFilePath = value;
                SaveSettings();
            }
        }

        [System.ComponentModel.SettingsBindable(true)]
        public static string BotKey
        {
            get { return Properties.Settings.Default.BotKey; }
            set
            {
                Properties.Settings.Default.BotKey = value;
                SaveSettings();
            }
        }

        [System.ComponentModel.SettingsBindable(true)]
        public static long AdminId
        {
            get { return Properties.Settings.Default.AdminId; }
            set
            {
                Properties.Settings.Default.AdminId = value;
                SaveSettings();
            }
        }

        [System.ComponentModel.SettingsBindable(true)]
        public static string DefaultImgFolder
        {
            get { return Properties.Settings.Default.DefaultImgFolder; }
            set
            {
                Properties.Settings.Default.DefaultImgFolder = value;
                SaveSettings();
            }
        }

        [System.ComponentModel.SettingsBindable(true)]
        public static Point InitialPosition
        {
            get { return Properties.Settings.Default.InitialPosition; }
            set
            {
                Properties.Settings.Default.InitialPosition = value;
                SaveSettings();
            }
        }

        [System.ComponentModel.SettingsBindable(true)]
        public static Size InitialSize
        {
            get { return Properties.Settings.Default.InitialSize; }
            set
            {
                Properties.Settings.Default.InitialSize = value;
                SaveSettings();
            }
        }

        [System.ComponentModel.SettingsBindable(true)]
        public static Dictionary<string, bool> Services
        {
            get
            {
                StringCollection strings = Properties.Settings.Default.Services;
                Dictionary<string, bool> services = new Dictionary<string, bool>();
                if (strings != null && strings.Count > 0)
                {
                    foreach (string s in strings)
                    {
                        string[] serviceProp = s.Split('=');
                        services[serviceProp[0]] = bool.Parse(serviceProp[1]);
                    }
                }
                return services;
            }
            set
            {
                StringCollection strings = new StringCollection();
                foreach (var item in value)
                {
                    strings.Add($"{item.Key}={item.Value}");
                }
                Properties.Settings.Default.Services = strings;
                SaveSettings();
            }
        }

        private static void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }
    }
}
