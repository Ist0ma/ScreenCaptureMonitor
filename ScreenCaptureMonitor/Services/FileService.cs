using ScreenCaptureMonitor.Interfaces;
using ScreenCaptureMonitor.ServicesSettings;
using ScreenCaptureMonitor.Static;

namespace ScreenCaptureMonitor.Services
{
    public class FileService : IService
    {
        private string _folderPath;

        public string Name => "File Service";
        public List<IServiceSetting> ServiceSettings => new List<IServiceSetting>() { new FileServiceFolderSetting(this) };

        public FileService()
        {
            if (string.IsNullOrEmpty(AppSettings.DefaultImgFolder) || !Directory.Exists(AppSettings.DefaultImgFolder))
            {
                try
                {
                    string rootPath = Directory.GetCurrentDirectory();
                    var dir = Directory.CreateDirectory(rootPath + "\\Screenshots");
                    AppSettings.DefaultImgFolder = dir.FullName;
                }
                catch (Exception e)
                {
                    ChooseNewFolder();
                }
            }
            _folderPath = AppSettings.DefaultImgFolder;
        }

        public void Execute(Bitmap screenshot)
        {
            string filePath = $"{_folderPath}/{DateTime.Now.ToString("dd_HH_MM_ss")}.jpg";
            screenshot.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
        }

        public void ChooseNewFolder()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            folderBrowserDialog.Description = "Choose a folder to save screenshots";
            folderBrowserDialog.ShowNewFolderButton = true;

            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string selectedFolderPath = folderBrowserDialog.SelectedPath;
                AppSettings.DefaultImgFolder = selectedFolderPath;
                _folderPath = selectedFolderPath;
            }
            else
            {
                ChooseNewFolder();
            }
        }
    }
}
