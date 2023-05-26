namespace ScreenCaptureMonitor.AdditionalForms
{
    public partial class TramsparentScrenshootAreaForm : Form
    {
        private Point startPoint;
        private Rectangle selectionRectangle;
        private bool isMouseDown;

        private MainForm _mainForm;
        public TramsparentScrenshootAreaForm(MainForm mainForm)
        {
            _mainForm = mainForm;

            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            Location = new Point(0, 0);
            Size = new Size(_mainForm.CurrentScreen.Bounds.Width, _mainForm.CurrentScreen.Bounds.Height);
            StartPosition = FormStartPosition.Manual;
            Opacity = 0.2;
            TopMost = true;

            MouseDown += MainForm_MouseDown;
            MouseMove += MainForm_MouseMove;
            MouseUp += MainForm_MouseUp;
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
                isMouseDown = true;
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                int x = Math.Min(startPoint.X, e.X);
                int y = Math.Min(startPoint.Y, e.Y);
                int width = Math.Abs(startPoint.X - e.X);
                int height = Math.Abs(startPoint.Y - e.Y);
                selectionRectangle = new Rectangle(x, y, width, height);

                Invalidate();
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isMouseDown)
            {
                isMouseDown = false;

                _mainForm.observer.CapturedArea = RectangleToScreen(selectionRectangle);

                selectionRectangle = Rectangle.Empty;

                Hide();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (isMouseDown)
            {
                e.Graphics.DrawRectangle(Pens.Red, selectionRectangle);
            }
        }
    }
}
