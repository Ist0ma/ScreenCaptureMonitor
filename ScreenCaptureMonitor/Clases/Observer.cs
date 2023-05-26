namespace ScreenCaptureMonitor.Clases
{
    public class Observer
    {
        private bool _autoReset = true;
        public bool AutoReset
        {
            get
            {
                return _autoReset;
            }
            set
            {
                _autoReset = value;
            }
        }
        private Rectangle _bounds;
        public Rectangle Bounds
        {
            get
            {
                return _bounds;
            }
            set
            {
                _bounds = value;
            }
        }
        private Bitmap _previousScreenImage;
        public static System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        public static event Action TimerStopped;
        private MainForm _mainForm;
        public delegate void ScreenChangeHandler(Bitmap screenshot);
        public Rectangle CapturedArea { get; set; }

        public event ScreenChangeHandler OnScreenChanges;

        public Observer(MainForm mainForm)
        {
            _mainForm = mainForm;
            timer = new();
            timer.Interval = 500;
            timer.Tick += Timer_Tick;
            CapturedArea = Screen.PrimaryScreen.Bounds;
        }

        public void Start()
        {
            timer.Start();
        }
        public void Stop()
        {
            timer.Stop();
            TimerStopped?.Invoke();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Bitmap currentScreenImage = new Bitmap(_bounds.Width, _bounds.Height);
            using (Graphics graphics = Graphics.FromImage(currentScreenImage))
            {
                graphics.CopyFromScreen(_bounds.Left, _bounds.Top, 0, 0, _bounds.Size);
            }

            if (_previousScreenImage != null && !AreBitmapsEqual(_previousScreenImage, currentScreenImage))
            {
                _previousScreenImage = null;

                Screen screen = Screen.PrimaryScreen;

                Bitmap screenshot = new Bitmap(CapturedArea.Width, CapturedArea.Height);

                _mainForm.StartButton.Visible = false;

                using (Graphics graphics = Graphics.FromImage(screenshot))
                {
                    graphics.CopyFromScreen(CapturedArea.X, CapturedArea.Y, 0, 0, CapturedArea.Size);
                }

                _mainForm.StartButton.Visible = true;

                OnScreenChanges?.Invoke(screenshot);
                if (!AutoReset)
                {
                    _mainForm.Stop();
                    _mainForm.isStarted = false;
                    Stop();
                }
            }

            _previousScreenImage = currentScreenImage;
        }

        private bool AreBitmapsEqual(Bitmap bitmap1, Bitmap bitmap2)
        {
            for (int x = 0; x < bitmap1.Width; x++)
            {
                for (int y = 0; y < bitmap1.Height; y++)
                {
                    if (bitmap1.GetPixel(x, y) != bitmap2.GetPixel(x, y))
                    {
                        //bitmap1.Save($"Prev_{DateTime.Now.ToString("dd_HH_MM_ss")}.jpg", System.Drawing.Imaging.ImageFormat.Png);
                        //bitmap2.Save($"New_{DateTime.Now.ToString("dd_HH_MM_ss")}.jpg", System.Drawing.Imaging.ImageFormat.Png);
                        //Bitmap diffBitmap = HighlightDifferences(bitmap1, bitmap2);
                        //diffBitmap.Save($"Diff_{DateTime.Now.ToString("dd_HH_MM_ss")}.jpg", System.Drawing.Imaging.ImageFormat.Png);
                        return false;
                    }
                }
            }

            return true;
        }
        private Bitmap HighlightDifferences(Bitmap bitmap1, Bitmap bitmap2)
        {
            Bitmap diffBitmap = bitmap1;

            for (int y = 0; y < bitmap1.Height; y++)
            {
                for (int x = 0; x < bitmap1.Width; x++)
                {
                    Color color1 = bitmap1.GetPixel(x, y);
                    Color color2 = bitmap2.GetPixel(x, y);

                    if (color1 != color2)
                        diffBitmap.SetPixel(x, y, Color.Red);
                }
            }

            return diffBitmap;
        }
    }
}
