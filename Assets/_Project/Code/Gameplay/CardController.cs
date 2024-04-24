using System;
using UnityEngine;

namespace AlexDev.LapTap
{
    [RequireComponent(typeof(Animator))]
    public class CardController : MonoBehaviour
    {

        #region Serialize Private Fields

        [SerializeField] private Renderer _cardRenderer;

        #endregion

        #region Public Fields

        public int column { get; private set; }
        public int row { get; private set; }

        #endregion

        #region Private Fields

        private bool _isOpen;
        private bool _isDone;


        private Animator _animator;

        #endregion

        public void Initialize(Texture2D cardTexture, int column, int row, bool isOpen = false, bool isDone = false)
        {
            _cardRenderer.materials[2].SetTexture("_BaseMap", cardTexture);
            this.column = column;
            this.row = row;
            _isOpen = isOpen;
            _isDone = isDone;
        }

        #region Events

        public event Action<int, int> CardSwitchedEvent;

        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _animator.SetBool("isOpen", _isOpen);
            _animator.SetBool("isDone", _isDone);
            
        }

        private void OnMouseUp()
        {
            if (_isOpen) return;
            _isOpen = true;
            OpenCard();
            CardSwitchedEvent?.Invoke(column, row);
        }

        #endregion

        #region Public Methods

        public void HideCard(bool isDone)
        {
            if (isDone) DestroyCard();
            else CloseCard();
        }

        #endregion

        #region Private Methods

        private void CloseCard()
        {
            _isOpen = false;
            _animator.SetBool("isOpen", false);
        }

        private void DestroyCard()
        {
            _isDone = true;
            _animator.SetBool("isDone", true);
        }

        private void OpenCard()
        {
            _animator.SetBool("isOpen", true);
        }

        #endregion

    }
}
