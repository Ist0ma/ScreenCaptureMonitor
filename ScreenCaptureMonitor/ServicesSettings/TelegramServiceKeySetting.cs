using ScreenCaptureMonitor.Enums;
using ScreenCaptureMonitor.Interfaces;
using ScreenCaptureMonitor.Static;

namespace ScreenCaptureMonitor.Services
{
    public class TelegramServiceKeySetting : IServiceSetting
    {
        private TelegramService _telegramService;

        public TelegramServiceKeySetting(TelegramService telegramService)
        {
            _telegramService = telegramService;
        }

        public string Name => _telegramService.BotKey;

        public string Description => "Enter Bot Key";

        public SettingType Type => SettingType.Text;

        public string AdditionalInfo => null;

        public int ControlWidth => 255;

        public void Action(object sender, EventArgs eventArgs)
        {
            AppSettings.BotKey = _telegramService.BotKey = ((TextBox)sender).Text;
        }
    }
}
