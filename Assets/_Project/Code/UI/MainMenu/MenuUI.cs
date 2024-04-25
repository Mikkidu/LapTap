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

        #endregion

        #region Events

        public event Action ContinueButtonPressedEvent;
        public event Action<int, int> StartButtonPressedEvent;

        #endregion

        #region MonoBehaviour Callbacks

        private void Start()
        {
            _continueButton.onClick.AddListener(OnContinueButtonPressed);
            _newGamePanel.StartGamePressedEvent += OnStartButtonPressed; 
        }

        #endregion

        #region Public Methods

        public void EnableContinueButton()
        {
            _continueButton.enabled = true;
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
