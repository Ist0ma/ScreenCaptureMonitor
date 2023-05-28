using ScreenCaptureMonitor.AdditionalForms;
using ScreenCaptureMonitor.Interfaces;
using ScreenCaptureMonitor.Static;

namespace ScreenCaptureMonitor.Builders
{
    public class SettingsPanelBuilder
    {
        Panel panel;
        MainForm mainForm;
        TramsparentScrenshootAreaForm areaForm;

        private List<IService> _services;

        private Dictionary<string, bool> defaultServices;

        private readonly int rowHeight = 25;
        public SettingsPanelBuilder(MainForm mainForm, List<IService> services)
        {
            _services = services;
            this.mainForm = mainForm;
            areaForm = new TramsparentScrenshootAreaForm(mainForm);
            defaultServices = AppSettings.Services;
        }

        public Panel CreatePanel()
        {
            panel = new Panel();
            int height = 0;
            int maxWidth = 0;
            foreach (IService service in _services)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.AutoSize = true;
                checkBox.Location = new Point(0, height);
                checkBox.TabIndex = 1;
                checkBox.Text = service.Name;
                if (AppSettings.Services.ContainsKey(service.Name))
                {
                    checkBox.Checked = AppSettings.Services[service.Name];
                    mainForm.observer.OnScreenChanges += service.Execute;
                }
                else
                {
                    checkBox.Checked = false;
                }
                checkBox.CheckedChanged += (sender, e) =>
                {
                    if (checkBox.Checked)
                    {
                        defaultServices[service.Name] = true;
                        AppSettings.Services = defaultServices;
                        mainForm.observer.OnScreenChanges += service.Execute;
                    }
                    else
                    {
                        defaultServices[service.Name] = false;
                        AppSettings.Services = defaultServices;
                        mainForm.observer.OnScreenChanges -= service.Execute;
                    }
                };
                height += 20;

                panel.Controls.Add(checkBox);

                foreach (IServiceSetting setting in service.ServiceSettings)
                {
                    int leftOffset = 0;
                    if (setting.Type == Enums.SettingType.Button)
                    {
                        Button button = new Button();

                        button.Location = new Point(25, height);
                        button.Size = new Size(setting.ControlWidth, rowHeight);
                        button.TabIndex = 2;
                        button.Text = setting.Name;
                        button.UseVisualStyleBackColor = true;
                        button.Click += setting.Action;
                        button.Click += (sender, e) =>
                        {
                            if (mainForm.isSettings)
                            {
                                mainForm.isSettings = false;
                                mainForm.SettingsHide();

                            }
                        };

                        panel.Controls.Add(button);

                        leftOffset = button.Location.X + button.Size.Width;
                    }
                    if (setting.Type == Enums.SettingType.Check)
                    {
                        CheckBox settingCheckBox = new CheckBox();

                        settingCheckBox.AutoSize = true;
                        settingCheckBox.Location = new Point(25, height);
                        settingCheckBox.Size = new Size(setting.ControlWidth, rowHeight);
                        settingCheckBox.TabIndex = 4;
                        settingCheckBox.Text = setting.Name;
                        settingCheckBox.UseVisualStyleBackColor = true;
                        settingCheckBox.CheckedChanged += setting.Action;

                        panel.Controls.Add(settingCheckBox);

                        leftOffset = settingCheckBox.Location.X + settingCheckBox.Size.Width;
                    }
                    if (setting.Type == Enums.SettingType.Text)
                    {
                        TextBox textBox = new TextBox();

                        textBox.AutoSize = true;
                        textBox.Location = new Point(25, height);
                        textBox.Size = new Size(setting.ControlWidth, rowHeight);
                        textBox.TabIndex = 4;
                        textBox.Text = setting.Name;
                        textBox.TextChanged += setting.Action;

                        panel.Controls.Add(textBox);

                        leftOffset = textBox.Location.X + textBox.Size.Width;
                    }

                    Label description = new Label();

                    description.Location = new Point(leftOffset, height + 5);
                    description.TabIndex = 1;
                    description.Text = setting.Description;
                    description.AutoSize = true;

                    panel.Controls.Add(description);

                    leftOffset += description.Width;

                    Label additional = new Label();

                    additional.Location = new Point(leftOffset, height + 7);
                    additional.TabIndex = 1;
                    additional.Text = setting.AdditionalInfo;
                    additional.Font = new Font("Arial", 8, FontStyle.Italic);
                    additional.ForeColor = Color.FromArgb(100, 100, 100);
                    additional.AutoSize = true;

                    panel.Controls.Add(additional);

                    leftOffset += additional.Width;
                    if (maxWidth < leftOffset)
                    {
                        maxWidth = leftOffset;
                    }

                    height += 35;
                }

                panel.Controls.Add(CreateLine(new Point(0, height)));

                height += 5;
            }

            RadioButton useMonitorButton = new RadioButton();
            useMonitorButton.Location = new Point(0, height);
            useMonitorButton.Text = "Take screenshot from monitor";
            useMonitorButton.Width = 200;
            useMonitorButton.Checked = true;
            useMonitorButton.CheckedChanged += (sender, e) =>
            {
                if (useMonitorButton.Checked)
                {
                    mainForm.observer.CapturedArea = Screen.FromHandle(mainForm.Handle).Bounds;
                }
            };

            panel.Controls.Add(useMonitorButton);

            height += rowHeight;

            RadioButton useSpecialAreaButton = new RadioButton();
            useSpecialAreaButton.Location = new Point(0, height);
            useSpecialAreaButton.Text = "Take screenshot from specified area";
            useSpecialAreaButton.Width = 250;
            useSpecialAreaButton.CheckedChanged += (sender, e) =>
            {
                if (useSpecialAreaButton.Checked)
                {
                    areaForm.Show();
                    areaForm.Bounds = Screen.FromControl(mainForm).Bounds;
                }
            };

            panel.Controls.Add(useSpecialAreaButton);

            height += rowHeight;


            CheckBox AutoResetCheckBox = new CheckBox();

            AutoResetCheckBox.AutoSize = true;
            AutoResetCheckBox.Location = new Point(0, height);
            AutoResetCheckBox.Size = new Size(75, rowHeight);
            AutoResetCheckBox.TabIndex = 4;
            AutoResetCheckBox.Text = "Auto Reset";
            AutoResetCheckBox.UseVisualStyleBackColor = true;
            AutoResetCheckBox.Checked = true;
            AutoResetCheckBox.CheckedChanged += (sender, e) =>
            {
                if (AutoResetCheckBox.Checked)
                {
                    mainForm.observer.AutoReset = true;
                }
                else
                {
                    mainForm.observer.AutoReset = false;
                }
            };

            panel.Controls.Add(AutoResetCheckBox);


            foreach (Control control in panel.Controls)
            {
                if (control is Label && control.Name.StartsWith("line"))
                {
                    control.Width = maxWidth;
                }
            }

            panel.AutoSize = true;
            panel.Cursor = Cursors.Default;

            return panel;
        }

        private Label CreateLine(Point location)
        {
            Label line = new Label();
            line.Name = "line";
            line.BorderStyle = BorderStyle.Fixed3D;
            line.Location = location;
            line.Size = new Size(150, 2);

            return line;
        }
    }
}
