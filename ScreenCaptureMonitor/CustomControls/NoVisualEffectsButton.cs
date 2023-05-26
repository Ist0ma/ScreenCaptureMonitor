namespace ScreenCaptureMonitor.CustomControls
{
    public class NoVisualEffectsButton : Button
    {
        public NoVisualEffectsButton()
        {
            SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true);
        }


        protected override void OnMouseEnter(EventArgs e)
        {
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
        }

        protected override void OnMouseLeave(EventArgs e)
        {
        }
    }
}
