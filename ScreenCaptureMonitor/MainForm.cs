using ScreenCaptureMonitor.Builders;
using ScreenCaptureMonitor.Clases;
using ScreenCaptureMonitor.CustomControls;
using ScreenCaptureMonitor.Interfaces;
using ScreenCaptureMonitor.Static;

namespace ScreenCaptureMonitor
{
    public class MainForm : Form
    {
        private readonly int _formBorder = 8;

        private bool _isDown = false;
        private Point _initialMousePosition;
        private Panel _settingsPanel;
        private SettingsPanelBuilder _panelBuilder;
        private NoVisualEffectsButton _settingsButton;
        public bool IsStarted { get; set; } = false;
        public bool IsSettings { get; set; } = false;
        public Observer Observer { get; set; }
        public NoVisualEffectsButton StartButton { get; set; }

        public Screen CurrentScreen
        {
            get
            {
                return Screen.FromHandle(Handle);
            }
        }

        public MainForm(List<IService> services)
        {
            InitializeComponent();

            Observer = new Observer(this);
            Observer.CapturedArea = Screen.FromHandle(Handle).Bounds;

            Location = AppSettings.InitialPosition;
            Size = AppSettings.InitialSize;

            _panelBuilder = new SettingsPanelBuilder(this, services);
            _settingsPanel = _panelBuilder.CreatePanel();
            _settingsPanel.Hide();
            _settingsPanel.Location = new Point(StartButton.Width + 10, 0);
            Controls.Add(_settingsPanel);
        }

        #region FormMovement
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (!IsSettings && !IsStarted)
            {
                _isDown = true;
                _initialMousePosition = PointToClient(MousePosition);
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDown)
            {
                Opacity = 0.2;
                Location = new Point(MousePosition.X - _initialMousePosition.X, MousePosition.Y - _initialMousePosition.Y);
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            _isDown = false;
            Opacity = 1;
            AppSettings.InitialPosition = Location;
        }

        private void MainForm_ResizeBegin(object sender, EventArgs e)
        {
            Opacity = 0.2;
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            Opacity = 1;
            AppSettings.InitialSize = Size;
        }

        #endregion

        private void StartButton_Click(object sender, EventArgs e)
        {
            IsStarted = !IsStarted;
            if (IsStarted)
            {
                Start();
            }
            else
            {
                Stop();
            }
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            IsSettings = !IsSettings;
            if (IsSettings)
            {
                SettingsShow();
            }
            else
            {
                SettingsHide();
            }
        }

        private void SetMainFormOnScreen()
        {
            Rectangle formBounds = Bounds;

            if (formBounds.Right > CurrentScreen.Bounds.Right || formBounds.Bottom > CurrentScreen.Bounds.Bottom)
            {
                int newX = Math.Min(formBounds.Left, CurrentScreen.Bounds.Right - formBounds.Width);
                int newY = Math.Min(formBounds.Top, CurrentScreen.Bounds.Bottom - formBounds.Height);

                Location = new Point(newX, newY);
            }
        }

        private void SetElements()
        {
            if (FormBorderStyle == FormBorderStyle.None)
            {
                MoveControls(new Point(_formBorder, _formBorder));
            }
            if (FormBorderStyle == FormBorderStyle.Sizable)
            {
                MoveControls(new Point(-_formBorder, -_formBorder));
            }
        }

        private void MoveControls(Point offset)
        {
            foreach (Control control in Controls)
            {
                control.Location = new Point(control.Location.X + offset.X, control.Location.Y + offset.Y);
            }
        }

        public void Start()
        {
            StartButton.Text = "Stop";
            Observer.Bounds = Bounds;
            Observer.Start();
            _settingsPanel.Visible = false;
            TransparencyKey = BackColor;
            FormBorderStyle = FormBorderStyle.None;
            _settingsButton.Visible = false;
            SetElements();
        }
        public void Stop()
        {
            Observer.Stop();
            StartButton.Text = "Start";
            Opacity = 1;
            TransparencyKey = Color.Wheat;
            FormBorderStyle = FormBorderStyle.Sizable;
            _settingsButton.Visible = true;
            SetElements();
        }


        public void SettingsShow()
        {
            _settingsPanel.Show();

            StartButton.Enabled = false;
            AutoSize = true;
            SetMainFormOnScreen();
            FormBorderStyle = FormBorderStyle.None;
            Cursor = Cursors.Default;
            SetElements();
        }
        public void SettingsHide()
        {
            StartButton.Enabled = true;
            _settingsPanel.Hide();
            AutoSize = false;
            Size = AppSettings.InitialSize;
            Location = AppSettings.InitialPosition;
            FormBorderStyle = FormBorderStyle.Sizable;
            Cursor = Cursors.SizeAll;
            SetElements();
        }

        private void InitializeComponent()
        {
            StartButton = new NoVisualEffectsButton();
            _settingsButton = new NoVisualEffectsButton();
            SuspendLayout();
            // 
            // StartButton
            // 
            StartButton.Cursor = Cursors.Hand;
            StartButton.Location = new Point(0, 0);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(60, 30);
            StartButton.TabIndex = 0;
            StartButton.Text = "Start";
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButton_Click;
            // 
            // SettingsButton
            // 
            _settingsButton.BackgroundImage = Properties.Resources.settings_button;
            _settingsButton.BackgroundImageLayout = ImageLayout.Stretch;
            _settingsButton.Cursor = Cursors.Hand;
            _settingsButton.Location = new Point(0, 36);
            _settingsButton.Name = "SettingsButton";
            _settingsButton.Size = new Size(30, 30);
            _settingsButton.TabIndex = 1;
            _settingsButton.UseVisualStyleBackColor = true;
            _settingsButton.Click += SettingsButton_Click;
            // 
            // MainForm
            // 
            ClientSize = new Size(84, 84);
            ControlBox = false;
            Controls.Add(_settingsButton);
            Controls.Add(StartButton);
            Cursor = Cursors.SizeAll;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            MinimumSize = new Size(100, 100);
            Name = "MainForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            ResizeBegin += MainForm_ResizeBegin;
            ResizeEnd += MainForm_ResizeEnd;
            MouseDown += MainForm_MouseDown;
            MouseMove += MainForm_MouseMove;
            MouseUp += MainForm_MouseUp;
            ResumeLayout(false);
        }
    }
}
