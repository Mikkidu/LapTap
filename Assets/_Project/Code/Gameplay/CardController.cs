using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlexDev.LapTap
{
    [RequireComponent(typeof(Animator))]
    public class CardController : MonoBehaviour
    {

        #region Serialize Private Fields

        [SerializeField] private MeshRenderer _cardRenderer;

        #endregion

        #region Private Fields

        private bool _isOpen;
        private bool _isDone;
        private int[] _place;

        private Animator _animator;

        #endregion

        public void Initialize(int id, int column, int row, bool isOpen = false, bool isDone = false)
        {
            _place = new int[2] { column, row };
            _isOpen = isOpen;
            _isDone = isDone;
        }

        #region Events

        public event Action<int[]> CardSwitchedEvent;

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
            CardSwitchedEvent?.Invoke(_place);
        }

        #endregion

        #region Public Methods

        public void CloseCard()
        {
            _isOpen = false;
            _animator.SetBool("isOpen", false);
        }

        public void DetroyCard()
        {
            _isDone = true;
            _animator.SetBool("isDone", true);
        }

        #endregion

        #region Private Methods

        private void OpenCard()
        {
            _animator.SetBool("isOpen", true);
        }

        #endregion

    }
}
