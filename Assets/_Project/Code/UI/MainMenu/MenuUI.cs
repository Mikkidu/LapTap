using AlexDev.CatchMe.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AlexDev.LapTap
{
    public class MenuUI : MonoBehaviour
    {
        #region Serialize Private Fields

        [SerializeField] private Button _continueButton;
        [SerializeField] private NewGamePanelUI _newGamePanel;
        [SerializeField] private SettingsPanelUI _settingsUI;

        #endregion

        #region Private Fields

        private AudioManager _audioManager;

        #endregion

        #region Events

        public event Action ContinueButtonPressedEvent;
        public event Action ExitButtonPressedEvent;
        public event Action<int, int> StartButtonPressedEvent;

        #endregion

        #region MonoBehaviour Callbacks

        private void Start()
        {
            _audioManager = AudioManager.instance;
            _settingsUI.Initialize();

            _continueButton.onClick.AddListener(OnContinueButtonPressed);
            _newGamePanel.StartGamePressedEvent += OnStartButtonPressed; 
        }

        #endregion

        #region Public Methods

        public void EnableContinueButton()
        {
            _continueButton.interactable = true;
        }

        public void OnExitButtonPressed()
        {
            ExitButtonPressedEvent?.Invoke();
        }

        public void PlayPressSound()
        {
            _audioManager.PlaySound("PRS");
        }

        public void PlayApplySound()
        {
            _audioManager.PlaySound("APL");

        }

        #endregion

        #region Private Methods

        private void OnContinueButtonPressed()
        {
            ContinueButtonPressedEvent?.Invoke();
        }

        private void OnStartButtonPressed(int columns, int rows)
        {
            StartButtonPressedEvent?.Invoke(columns, rows);
        }

        #endregion
    }
}
