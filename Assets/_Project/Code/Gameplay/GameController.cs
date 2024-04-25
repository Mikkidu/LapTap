
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AlexDev.LapTap
{
    public class GameController : MonoBehaviour
    {

        #region Serialize Private Fields

        [SerializeField] private CardsBuilder _builder;
        [SerializeField] private GameUI _gameUI;

        [Range(2, 6)]
        [SerializeField] private int _columns = 2;

        [Range(2, 6)]
        [SerializeField] private int _rows = 2;

        #endregion

        #region Private Fields

        private int _currentTurn;
        private int _score;
        private int _hiScore;
        private string _recordName = "";
        private CardData[,] _cardDatas;
        private CardController[,] _cardControllers;
        private int _lastMatchTurn;
        private CardController _previousCardController;
        private int _bonus;
        private int _comboCounts;
        private int _cardLeft;

        #endregion

        #region Events

        public event Action<int> TurnChangedEvent;
        public event Action<int> ScoreChangedEvent;
        public event Action<int, int> ComboChangedEvent;

        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            ScoreChangedEvent += _gameUI.UpdateScore;
            TurnChangedEvent += _gameUI.UpdateTurn;
            ComboChangedEvent += _gameUI.UpdateComboCount;

            _gameUI.MainMenuButtonPressedEvent += ExitToMainMenu;
            _gameUI.RecordPlayerNameEnteredEvent += SaveRecord;
        }

        void Start()
        {
            ScoreChangedEvent?.Invoke(_score);
            TurnChangedEvent?.Invoke(_currentTurn);
            ComboChangedEvent?.Invoke(_comboCounts, _bonus);
            _gameUI.SetHiScore(_hiScore, _recordName);
            _cardLeft = _columns * _rows;
            _cardDatas = new CardData[_columns, _rows];
            _cardControllers = _builder.BuildCards(ref _cardDatas);
            foreach (var card in _cardControllers)
            {
                card.CardSwitchedEvent += CheckCard;
            }
        }

        #endregion

        #region Public Methods

        public void CheckCard(int column, int row)
        {
            _currentTurn++;
            TurnChangedEvent?.Invoke(_currentTurn);
            if ((_currentTurn % 2) == 0)
            {
                EvenTurn(column, row);
            }
            else
            {
                OddTurn(column, row);
            }
        }

        #endregion

        #region Private Methods

        private void EvenTurn(int column, int row)
        {
            bool isDone;
            if (_cardDatas[_previousCardController.column, _previousCardController.row].id == _cardDatas[column, row].id)
            {
                isDone = true;
                if (_currentTurn == _lastMatchTurn + 2 && _currentTurn > 2)
                {
                    _bonus *= 2;
                }
                else
                {
                    _bonus = 1;
                }
                _score += _bonus;
                ScoreChangedEvent?.Invoke(_score);
                _lastMatchTurn = _currentTurn;
                _comboCounts++;
                _cardLeft -= 2;
                if (_cardLeft <= 0)
                {
                    GameOver();
                }
            }
            else
            {
                isDone = false;
                _comboCounts = 0;
                _bonus = 0;
            }
            ComboChangedEvent?.Invoke(_comboCounts, _bonus);
            StartCoroutine(HideCards(isDone, _previousCardController, _cardControllers[column, row]));
        }

        private void OddTurn(int column, int row)
        {
            _previousCardController = _cardControllers[column, row];
        }

        private void GameOver()
        {
            if (_score > _hiScore)
            {
                _gameUI.ShowNameInputPanel(_score, _currentTurn);
            }
            else
            {
                _gameUI.ShowRezultsScreen(_score, _currentTurn);
            }
        }

        private void ExitToMainMenu()
        {
            if (_cardLeft > 0)
            {
                SaveGame();
            }
            SceneManager.LoadScene(0);
        }

        private void SaveGame()
        {

        }

        private void SaveRecord(string name)
        {

        }

        #endregion

        #region Coroutines

        IEnumerator HideCards(bool isDone, params CardController[] cardControllers)
        {
            yield return new WaitForSeconds(1);
            foreach(var card in cardControllers)
            {
                card.HideCard(isDone);
            }
        }

        #endregion
    }
}
