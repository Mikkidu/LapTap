using System;
using UnityEngine;
using UnityEngine.Audio;


namespace AlexDev.CatchMe.Audio
{
    public class AudioManager : MonoBehaviour
    {
        #region Public Fields

        public static AudioManager instance;

        public const string SETTINGS_VOLUME_MUSIC = "MusicVolume";
        public const string SETTINGS_VOLUME_SFX = "SfxVolume";

        public bool isMusicOn { get; private set; } = true;
        public bool isSfxOn { get; private set; } = true;
        public float currentMusicVolume { get; private set; }
        public float currentSfxVolume { get; private set; }

        #endregion

        #region Private Serialize Fields

        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _sfxSource;
        [SerializeField] private Sound[] _sounds;
        [SerializeField] private Sound[] _tracks;

        #endregion

        #region Private Fields


        #endregion

        #region Monobehaviour Callbacks
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        #region Publick Methods

        public void InitializeVolume(float musicVolume, bool isMusicOn, float sfxVolume, bool isSfxOn)
        {
            SetMusicVolume(musicVolume);
            SetSFXVolume(sfxVolume);
            SwitchOnMusic(isMusicOn);
            SwitchOnSfx(isSfxOn);
        }

        private void PlayMusic(Sound track)
        {
            _musicSource.clip = track.clip;
            _musicSource.Play();
        }

        public void PlayMusic(string trackName)
        {
            Sound track = Array.Find(_tracks, tracks => tracks.Name == trackName);
            PlayMusic(track);
        }

        public void PlayMusicIfAnother(string trackName)
        {
            Sound track = Array.Find(_tracks, tracks => tracks.Name == trackName);
            if (track.clip == _musicSource.clip)
                return;
            PlayMusic(track);
        }

        public void PlaySound(string soundName)
        {
            Sound sound = Array.Find(_sounds, sounds => sounds.Name == soundName);
            if (sound != null)
                _sfxSource.PlayOneShot(sound.clip, sound.volume);
        }

        public void SetMusicVolume(float volume)
        {
            currentMusicVolume = volume;
            if (!isMusicOn) return;
            _mixer.SetFloat(SETTINGS_VOLUME_MUSIC, Mathf.Log10(volume) * 20);
        }

        public void SetSFXVolume(float volume)
        {
            currentSfxVolume = volume;
            if (!isSfxOn) return;
            _mixer.SetFloat(SETTINGS_VOLUME_SFX, Mathf.Log10(volume) * 20);
        }

        public void SwitchOnMusic(bool isOn)
        {
            isMusicOn = isOn;
            _mixer.SetFloat(SETTINGS_VOLUME_MUSIC, Mathf.Log10(currentMusicVolume * (isOn ? 1 : 0.00001f)) * 20);
        }

        public void SwitchOnSfx(bool isOn)
        {
            isSfxOn = isOn;
            _mixer.SetFloat(SETTINGS_VOLUME_SFX, Mathf.Log10(currentSfxVolume * (isOn ? 1 : 0.00001f)) * 20);
        }

        #endregion
    }
}
