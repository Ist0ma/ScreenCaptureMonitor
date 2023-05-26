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

        public static void LoadSettings()
        {
            Properties.Settings.Default.Reload();
        }

        private static void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }
    }
}
