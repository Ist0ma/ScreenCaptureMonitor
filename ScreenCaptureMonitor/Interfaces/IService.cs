namespace ScreenCaptureMonitor.Interfaces
{
    public interface IService
    {
        string Name { get; }
        List<IServiceSetting> ServiceSettings { get; }
        void Execute(Bitmap screenshot);
    }
}
