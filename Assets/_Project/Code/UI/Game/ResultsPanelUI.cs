using System;
using TMPro;
using UnityEngine;

namespace AlexDev.LapTap
{
    public class ResultsPanelUI : MonoBehaviour
    {
        #region Serialize Private Fields

        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _turnsText;

        #endregion

        #region Events

        public event Action MainMenuButtonPressedEvent;

        #endregion

        #region Public Methods

        public void Initialize(int score, int turns)
        {
            _scoreText.text = score.ToString();
            _turnsText.text = turns.ToString();
        }

        public void OnMainMenuButtonPressed()
        {
            MainMenuButtonPressedEvent.Invoke();
        }

        #endregion
    }
}
