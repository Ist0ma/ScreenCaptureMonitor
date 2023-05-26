using ScreenCaptureMonitor.Enums;
using ScreenCaptureMonitor.Interfaces;
using ScreenCaptureMonitor.Services;
using ScreenCaptureMonitor.Static;

namespace ScreenCaptureMonitor.ServicesSettings
{
    public class FileServiceFolderSetting : IServiceSetting
    {
        private FileService _fileService;

        public FileServiceFolderSetting(FileService fileService)
        {
            _fileService = fileService;
        }

        public string Name => "Choose";

        public string Description => "Choose a folder to save screenshots";

        public SettingType Type => SettingType.Button;

        public string AdditionalInfo => AppSettings.DefaultImgFolder;

        public int ControlWidth => 60;

        public void Action(object sender, EventArgs eventArgs)
        {
            _fileService.ChooseNewFolder();
        }
    }
}
