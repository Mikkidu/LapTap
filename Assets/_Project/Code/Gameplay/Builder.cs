using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AlexDev.LapTap
{
    public class Builder : MonoBehaviour
    {
        #region Serialize Private Fields

        [Range(2, 6)]
        [SerializeField] private int _columns = 2;

        [Range(2, 6)]
        [SerializeField] private int _rows = 2;

        [SerializeField] private CardController _cardControllerPrefab;

        [SerializeField] private float _cardSize;
        [SerializeField] private float _gapSize;

        [SerializeField] private Texture2D[] _cardMaterials;

        #endregion

        #region Private Fields

        private Vector2 _cornerPosition;
        private float _spawnStep;
        private float _scaleFactor;

        #endregion

        #region MonoBehaviourCallbacks

        void Start()
        {
            BuildCards(_columns, _rows);
        }

        #endregion

        #region Public Fields

        private CardController[,] BuildCards(int columnsNumber, int rowsNumber)
        {
            int maxLength = _columns > _rows ? _columns : _rows;
            _scaleFactor = 2f / maxLength;
            _spawnStep = (_cardSize + _gapSize) * _scaleFactor / 2f;

            float cornerX = -_spawnStep * _columns;
            float cornerY = -_spawnStep * _rows;
            _cornerPosition = new Vector3(cornerX, cornerY) + transform.position;

            CardData[,] cards = GenerateCardPack();

            return SpawnCards(cards);
        }

        #endregion

        #region Private Methods 

        private CardData[,] GenerateCardPack()
        {
            var cards = new CardData[_columns, _rows];
            int cardCount = _columns * _rows;
            int startID = Random.Range(0, _cardMaterials.Length - cardCount / 2);
            List<int> idList = Enumerable.Range(startID, cardCount / 2).ToList();
            idList.AddRange(idList);
            int randomIndex;
            for (int i = 0; i < _columns; i++)
            {
                for (int j = 0; j < _rows; j++)
                {
                    randomIndex = Random.Range(0, idList.Count);
                    cards[i, j] = new CardData(idList[randomIndex]);
                    idList.RemoveAt(randomIndex);
                }
            }
            return cards;
        }

        private CardController[,] SpawnCards(CardData[,] cards)
        {
            CardController[,] cardController = 
                new CardController[cards.GetLength(0), cards.GetLength(1)];
            float x;
            float y;
            Vector2 spawnPosition;
            for (int i = 0; i < _columns; i++)
            {
                for (int j = 0; j < _rows; j++)
                {
                    x = _spawnStep * (i * 2f + 1f);
                    y = _spawnStep * (j * 2f + 1f);
                    spawnPosition = new Vector2(x, y) + _cornerPosition;
                    cardController[i, j] = Instantiate(
                        _cardControllerPrefab, 
                        spawnPosition, 
                        Quaternion.identity, 
                        transform);

                    cardController[i, j].transform.localScale *= _scaleFactor;
                    cardController[i, j].Initialize(
                        _cardMaterials[cards[i,j].id], 
                        i, 
                        j, 
                        cards[i, j].isOpen, 
                        cards[i, j].isDone);
                }
            }
            return cardController;
        }

        #endregion
    }

}
