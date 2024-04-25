using AlexDev.CatchMe.Audio;
using AlexDev.DataModule;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AlexDev.LapTap
{
    public class SettingsPanelUI : MonoBehaviour
    {
        #region Serialize Private Fields

        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _sfxToggle;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;

        #endregion

        #region Private Fields

        private DataManager _dataManager;
        private GameSettingsData _settingsData;
        private AudioManager _audioManager;

        #endregion


        #region Public Methods

        public void Initialize()
        {
            _dataManager = DataManager.instance;
            _audioManager = AudioManager.instance;

            _settingsData = _dataManager.gameSettings;

            _audioManager.SetMusicVolume(_settingsData.musicVolume);
            _audioManager.SetSFXVolume(_settingsData.sfxVolume);
            _audioManager.SwitchOnMusic(_settingsData.isMusicOn);
            _audioManager.SwitchOnSfx(_settingsData.isSfxOn);

            _musicSlider.value = _settingsData.musicVolume;
            _sfxSlider.value = _settingsData.sfxVolume;
            _musicToggle.isOn = _settingsData.isMusicOn;
            _sfxToggle.isOn = _settingsData.isSfxOn;
        }


        public void OnMusicToggleChange(bool isOn)
        {

            _audioManager.SwitchOnMusic(isOn);
            _settingsData.isMusicOn = isOn;
        }

        public void OnSfxToggleChange(bool isOn)
        {
            _audioManager.SwitchOnSfx(isOn);
            _settingsData.isSfxOn = isOn;
        }

        public void OnMusicVolumeChange(float volume)
        {
            _audioManager.SetMusicVolume(volume);
            _settingsData.musicVolume = volume;
        }


        public void OnSfxVolumeChange(float volume)
        {
            _audioManager.SetSFXVolume(volume);
            _settingsData.sfxVolume = volume;
        }

        public void SaveSettings()
        {
            _dataManager.SaveGameSettings();
        }

        #endregion
    }
}
