
using AlexDev.DataModule;
using System;
using System.Collections;
using System.Linq;
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

        private DataManager _dataManager;
        private GameData _gameData;

        private CardData[,] _cardDatas;
        private CardController[,] _cardControllers;
        private CardController _previousCardController;

        #endregion

        #region Events

        public event Action<int> TurnChangedEvent;
        public event Action<int> ScoreChangedEvent;
        public event Action<int, int> ComboChangedEvent;

        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            _dataManager = DataManager.instance;
            Initialize();

            ScoreChangedEvent += _gameUI.UpdateScore;
            TurnChangedEvent += _gameUI.UpdateTurn;
            ComboChangedEvent += _gameUI.UpdateComboCount;

            _gameUI.MainMenuButtonPressedEvent += ExitToMainMenu;
            _gameUI.RecordPlayerNameEnteredEvent += SaveRecord;
        }

        void Start()
        {

            ScoreChangedEvent?.Invoke(_gameData.score);
            TurnChangedEvent?.Invoke(_gameData.turn);
            ComboChangedEvent?.Invoke(_gameData.comboCount, _gameData.bonus);
            _gameUI.SetHiScore(_gameData.hiScore, _gameData.recordHolderName);

            if (_gameData.hasSavedGame)
            {
                _cardControllers = _builder.BuildCards(ref _cardDatas);
                _previousCardController = _cardControllers[_gameData.previousCardColumn, _gameData.previousCardRow];
            }
            else
            {
                _gameData.cardLeft = _gameData.columns * _gameData.rows;
                _cardControllers = _builder.BuildNewCards(ref _cardDatas);
            }

            foreach (var card in _cardControllers)
            {
                card.CardSwitchedEvent += CheckCard;
            }
        }

        #endregion

        #region Public Methods

        public void CheckCard(int column, int row)
        {
            _gameData.turn++;
            TurnChangedEvent?.Invoke(_gameData.turn);
            if ((_gameData.turn % 2) == 0)
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

        private void Initialize()
        {
            if (_dataManager.TryLoadData(out _gameData))
            {
                if (_gameData.hasSavedGame)
                {
                    if(_dataManager.TryLoadArrayData<CardData>(out CardData[] cardDatasArray))
                    {
                        _cardDatas = new CardData[_gameData.columns, _gameData.rows];
                        ConvertToMatrix(cardDatasArray, ref _cardDatas);
                        return;
                    }
                }
            }
            else
            {
                Debug.LogError(gameObject.name + "Have no saved game data! TestMode");
                _gameData = new GameData()
                {
                    columns = _columns,
                    rows = _rows
                };
            }
            _cardDatas = new CardData[_gameData.columns, _gameData.rows];
        }

        private void EvenTurn(int column, int row)
        {
            bool isDone;
            if (_cardDatas[_previousCardController.column, _previousCardController.row].id == _cardDatas[column, row].id)
            {
                isDone = true;
                if (_gameData.turn == _gameData.lastMatchTurn + 2 && _gameData.turn > 2)
                {
                    _gameData.bonus *= 2;
                }
                else
                {
                    _gameData.bonus = 1;
                }
                _gameData.score += _gameData.bonus;
                ScoreChangedEvent?.Invoke(_gameData.score);
                _gameData.lastMatchTurn = _gameData.turn;
                _gameData.comboCount++;
                _gameData.cardLeft -= 2;

                _cardDatas[column, row].isDone = true;
                _cardDatas[_previousCardController.column, _previousCardController.row].isDone = true;
                if (_gameData.cardLeft <= 0)
                {
                    GameOver();
                }
            }
            else
            {
                isDone = false;
                _gameData.comboCount = 0;
                _gameData.bonus = 0;
            }

            _cardDatas[column, row].isOpen = false;
            _cardDatas[_previousCardController.column, _previousCardController.row].isOpen = false;
            ComboChangedEvent?.Invoke(_gameData.comboCount, _gameData.bonus);
            StartCoroutine(HideCards(isDone, _previousCardController, _cardControllers[column, row]));
        }

        private void OddTurn(int column, int row)
        {
            _previousCardController = _cardControllers[column, row];
            _cardDatas[column, row].isOpen = true;
        }

        private void GameOver()
        {
            if (_gameData.score > _gameData.hiScore)
            {
                _gameUI.ShowNameInputPanel(_gameData.score, _gameData.turn);
            }
            else
            {
                _gameUI.ShowRezultsScreen(_gameData.score, _gameData.turn);
            }
        }

        private void ExitToMainMenu()
        {
            if (_gameData.cardLeft > 0)
            {
                SaveGame();
            }
            SceneManager.LoadScene(0);
        }

        private void SaveGame()
        {
            _gameData.hasSavedGame = true;
            _gameData.previousCardColumn = _previousCardController.column;
            _gameData.previousCardRow = _previousCardController.row;
            _dataManager.SaveData(_gameData);
            CardData[] saveCardData = _cardDatas.Cast<CardData>().ToArray();
            _dataManager.SaveArrayData<CardData>(saveCardData);
        }

        private void SaveRecord(string name)
        {
            var hiScoreData = new GameData()
            {
                hiScore = _gameData.score,
                recordHolderName = name
            };
            _dataManager.SaveData(hiScoreData);
        }

        private void ConvertToMatrix(CardData[] cardArray, ref CardData[,] cardMatrix)
        {
            int index = 0;
            for (int i = 0; i < cardMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < cardMatrix.GetLength(1); j++)
                {
                    cardMatrix[i, j] = cardArray[index];
                    index++;
                }
            }
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
