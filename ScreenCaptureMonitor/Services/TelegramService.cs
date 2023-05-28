using ScreenCaptureMonitor.Interfaces;
using ScreenCaptureMonitor.Static;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ScreenCaptureMonitor.Services
{
    public class TelegramService : IService
    {
        private string _botKey;
        private long _adminId;

        public string Name => "Telegram Service";
        public List<IServiceSetting> ServiceSettings => new List<IServiceSetting>() { new TelegramServiceKeySetting(this), new TelegramServiceAdminIdSetting(this) };

        public string BotKey
        {
            get { return _botKey; }
            set { _botKey = value; }
        }

        public long AdminId
        {
            get { return _adminId; }
            set { _adminId = value; }
        }

        public TelegramService()
        {
            if (string.IsNullOrEmpty(AppSettings.BotKey) || AppSettings.AdminId == 0)
            {
                EnterBotSettings();
            }

            BotKey = AppSettings.BotKey;
            AdminId = AppSettings.AdminId;
        }

        public void Execute(Bitmap screenshot)
        {
            var newStream = new MemoryStream();
            screenshot.Save(newStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            newStream.Position = 0;

            var inputFile = new InputFileStream(newStream);
            new TelegramBotClient(_botKey).SendPhotoAsync(_adminId, inputFile);
        }

        public void EnterBotSettings()
        {
            MessageBox.Show("Enter bot parameters in telegram service properties");
        }
    }
}
