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

        #endregion

        private void Start()
        {
            _hiScorePlayerNameText.rectTransform.ForceUpdateRectTransforms();
            _scoreText.rectTransform.ForceUpdateRectTransforms();
        }

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


        #endregion

    }
}
