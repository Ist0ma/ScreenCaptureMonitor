using ScreenCaptureMonitor.Enums;

namespace ScreenCaptureMonitor.Interfaces
{
    public interface IServiceSetting
    {
        string Name { get; }
        string Description { get; }
        string AdditionalInfo { get; }
        int ControlWidth { get; }
        SettingType Type { get; }
        void Action(object sender, EventArgs eventArgs);
    }
}
