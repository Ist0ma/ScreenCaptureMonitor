using ScreenCaptureMonitor.Interfaces;
using ScreenCaptureMonitor.Services;

namespace ScreenCaptureMonitor
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();


            List<IService> services = new List<IService>
            {
                new FileService(),
                new SoundService(),
                new TelegramService()
            };

            Application.Run(new MainForm(services));
        }
    }
}