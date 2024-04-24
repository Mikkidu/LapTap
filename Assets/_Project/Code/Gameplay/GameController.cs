
using System.Collections;
using UnityEngine;

namespace AlexDev.LapTap
{
    public class GameController : MonoBehaviour
    {

        #region Serialize Private Fields

        [SerializeField] private CardsBuilder _builder;

        [Range(2, 6)]
        [SerializeField] private int _columns = 2;

        [Range(2, 6)]
        [SerializeField] private int _rows = 2;

        #endregion

        #region Private Fields

        private int _currentTurn;
        private int _score;
        private CardData[,] _cardDatas;
        private CardController[,] _cardControllers;
        private int _lastMatchTurn;
        private CardController _previousCardController;
        private int _bonus = 1;

        #endregion

        #region MonoBehaviour Callbacks

        void Start()
        {
            _cardDatas = new CardData[_columns, _rows];
            _cardControllers = _builder.BuildCards(ref _cardDatas);
            foreach (var card in _cardControllers)
            {
                card.CardSwitchedEvent += CheckCard;
            }
        }

        void Update()
        {
        
        }

        #endregion

        #region Public Methods

        public void CheckCard(int column, int row)
        {
            _currentTurn++;
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
                _lastMatchTurn = _currentTurn;
            }
            else
            {
                isDone = false;
            }

            StartCoroutine(HideCards(isDone, _previousCardController, _cardControllers[column, row]));
        }

        private void OddTurn(int column, int row)
        {
            _previousCardController = _cardControllers[column, row];
        }

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
