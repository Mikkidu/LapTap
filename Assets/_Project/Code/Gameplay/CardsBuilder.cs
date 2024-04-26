using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AlexDev.LapTap
{
    public class CardsBuilder : MonoBehaviour
    {
        #region Serialize Private Fields

        [Range(2, 6)]
        [SerializeField] private int _columns = 2;

        [Range(2, 6)]
        [SerializeField] private int _rows = 2;

        [SerializeField] private CardController _cardControllerPrefab;

        [SerializeField] private float _cardSize;
        [SerializeField] private float _gapSize;

        [SerializeField] private Texture2D[] _cardTextures;

        #endregion

        #region Private Fields

        private Vector2 _cornerPosition;
        private float _spawnStep;
        private float _scaleFactor;

        #endregion

        #region Public Fields

        public CardController[,] BuildNewCards(ref CardData[,] cards)
        {
            GenerateCardPackID(ref cards);
            return BuildCards(ref cards);
        }

        public CardController[,] BuildCards(ref CardData[,] cards)
        {
            _columns = cards!.GetLength(0);
            _rows = cards!.GetLength(1);
            int maxLength = _columns > _rows ? _columns : _rows;
            _scaleFactor = 2f / maxLength;
            _spawnStep = (_cardSize + _gapSize) * _scaleFactor / 2f;

            float cornerX = -_spawnStep * _columns;
            float cornerY = -_spawnStep * _rows;
            _cornerPosition = new Vector3(cornerX, cornerY) + transform.position;

            return SpawnCards(cards);
        }

        #endregion

        #region Private Methods 

        private void GenerateCardPackID(ref CardData[,] cards)
        {
            int columns = cards!.GetLength(0);
            int rows = cards!.GetLength(1);
            int cardCount = columns * rows;
            int startID = Random.Range(0, _cardTextures.Length - cardCount / 2);
            List<int> idList = Enumerable.Range(startID, cardCount / 2).ToList();
            idList.AddRange(idList);
            int randomIndex;
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    randomIndex = Random.Range(0, idList.Count);
                    cards[i, j] = new CardData(idList[randomIndex]);
                    idList.RemoveAt(randomIndex);
                }
            }
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
                        _cardTextures[cards[i,j].id], 
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
