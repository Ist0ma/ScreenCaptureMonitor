using ScreenCaptureMonitor.Enums;
using ScreenCaptureMonitor.Interfaces;
using ScreenCaptureMonitor.Services;
using ScreenCaptureMonitor.Static;

namespace ScreenCaptureMonitor.ServicesSettings
{
    public class SoundServiceFileSetting : IServiceSetting
    {
        private SoundService _soundService;

        public SoundServiceFileSetting(SoundService soundService)
        {
            _soundService = soundService;
        }

        public string Name => "Choose";

        public string Description => "Choose sound file (.wav)";

        public string AdditionalInfo => AppSettings.SoundFilePath;

        public SettingType Type => SettingType.Button;

        public int ControlWidth => 60;

        public void Action(object sender, EventArgs eventArgs)
        {
            _soundService.ChooseNewFile();
        }
    }
}
