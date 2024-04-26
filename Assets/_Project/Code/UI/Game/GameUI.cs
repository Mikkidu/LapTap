using AlexDev.CatchMe.Audio;
using System;
using TMPro;
using UnityEngine;

namespace AlexDev.LapTap
{
    public class GameUI : MonoBehaviour
    {

        #region Serialize Private Fields

        [SerializeField] private TextMeshProUGUI _hiScoreText;
        [SerializeField] private TextMeshProUGUI _hiScorePlayerNameText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _turnCountText;
        [SerializeField] private TextMeshProUGUI _comboCountText;
        [SerializeField] private TextMeshProUGUI _bonusText;

        [SerializeField] private TextInputPanelUI _recordNameinputPanel;
        [SerializeField] private ResultsPanelUI _resultsPanel;
        [SerializeField] private SettingsPanelUI _settingsUI;

        #endregion

        #region Events

        public event Action<string> RecordPlayerNameEnteredEvent;
        public event Action MainMenuButtonPressedEvent;

        #endregion

        #region Private Fields

        private AudioManager _audioManager;

        #endregion

        #region MonoBehaviour Callbacks

        private void Start()
        {
            _audioManager = AudioManager.instance;
            _settingsUI.Initialize();

            _recordNameinputPanel.OnConfirmingTextEvent += OnRecordPlaernNameEntered;
            _resultsPanel.MainMenuButtonPressedEvent += OnMainMenuButtonPressed;
        }

        #endregion


        #region Public Methods

        public void SetHiScore(int hiScore, string playerName)
        {
            _hiScoreText.text = hiScore.ToString();
            _hiScorePlayerNameText.text = playerName;
        }

        public void UpdateScore(int score)
        {
            _scoreText.text = score.ToString();
        }

        public void UpdateTurn(int turnCount)
        {
            _turnCountText.text = (turnCount / 2).ToString();
        }

        public void UpdateComboCount(int comboCount, int bonusAdded)
        {
            _comboCountText.text = comboCount.ToString();
            _bonusText.text = "+" + bonusAdded;
        }

        public void ShowRezultsScreen(int score, int turns)
        {
            _resultsPanel.Initialize(score, turns / 2);
            _resultsPanel.gameObject.SetActive(true);
        }

        public void ShowNameInputPanel(int score, int turns)
        {
            _resultsPanel.Initialize(score, turns / 2);
            _recordNameinputPanel.SetPlaseholderText("cool broh");
            _recordNameinputPanel.gameObject.SetActive(true);
        }

        public void OnRecordPlaernNameEntered(string name)
        {
            RecordPlayerNameEnteredEvent?.Invoke(name);
        }

        public void OnMainMenuButtonPressed()
        {
            MainMenuButtonPressedEvent?.Invoke();
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


    }
}
