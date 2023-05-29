namespace ScreenCaptureMonitor.AdditionalForms
{
    public partial class TramsparentScrenshootAreaForm : Form
    {
        private bool _isMouseDown;
        private Point _startPoint;
        public Rectangle _selectionRectangle;

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
                _startPoint = e.Location;
                _isMouseDown = true;
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                int x = Math.Min(_startPoint.X, e.X);
                int y = Math.Min(_startPoint.Y, e.Y);
                int width = Math.Abs(_startPoint.X - e.X);
                int height = Math.Abs(_startPoint.Y - e.Y);
                _selectionRectangle = new Rectangle(x, y, width, height);

                Invalidate();
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _isMouseDown && _selectionRectangle.Size != Size.Empty)
            {
                _isMouseDown = false;

                _mainForm.Observer.CapturedArea = RectangleToScreen(_selectionRectangle);

                _selectionRectangle = Rectangle.Empty;

                Hide();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_isMouseDown)
            {
                e.Graphics.DrawRectangle(Pens.Red, _selectionRectangle);
            }
        }
    }
}
