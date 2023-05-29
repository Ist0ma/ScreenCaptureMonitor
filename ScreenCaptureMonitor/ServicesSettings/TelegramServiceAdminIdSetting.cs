using ScreenCaptureMonitor.Enums;
using ScreenCaptureMonitor.Interfaces;
using ScreenCaptureMonitor.Static;

namespace ScreenCaptureMonitor.Services
{
    public class TelegramServiceAdminIdSetting : IServiceSetting
    {
        private TelegramService _telegramService;

        public TelegramServiceAdminIdSetting(TelegramService telegramService)
        {
            _telegramService = telegramService;
        }

        public string Name => _telegramService.AdminId.ToString();

        public string Description => "Enter chat ID";

        public SettingType Type => SettingType.Number;

        public string AdditionalInfo => null;

        public int ControlWidth => 70;

        public void Action(object sender, EventArgs eventArgs)
        {
            if (!string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                AppSettings.AdminId = _telegramService.AdminId = long.Parse(((TextBox)sender).Text);
            }
        }
    }
}
