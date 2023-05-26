﻿using ScreenCaptureMonitor.Clases;
using ScreenCaptureMonitor.Interfaces;
using ScreenCaptureMonitor.ServicesSettings;
using ScreenCaptureMonitor.Static;
using System.Media;

namespace ScreenCaptureMonitor.Services
{
    public class SoundService : IService
    {
        private SoundPlayer _player;
        private string _soundFilePath;

        public string Name => "Sound Service";

        public List<IServiceSetting> ServiceSettings => new List<IServiceSetting>() { new SoundServiceFileSetting(this) };

        public SoundService()
        {
            _player = new SoundPlayer();

            if (string.IsNullOrEmpty(AppSettings.SoundFilePath) || !File.Exists(AppSettings.SoundFilePath))
            {
                ChooseNewFile();
            }
            _soundFilePath = AppSettings.SoundFilePath;
            if (File.Exists(_soundFilePath))
            {
                _player = new SoundPlayer(_soundFilePath);
            }
            Observer.TimerStopped += OnTimerStopped;
        }

        private void OnTimerStopped()
        {
            _player.Stop();
        }

        public void Execute(Bitmap screenshot)
        {
            _player.Play();
        }

        public void ChooseNewFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Sound files|*.wav";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                AppSettings.SoundFilePath = selectedFilePath;
                _soundFilePath = selectedFilePath;
            }
            else
            {
                ChooseNewFile();
            }
        }
    }
}