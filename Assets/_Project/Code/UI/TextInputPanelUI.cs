using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AlexDev.LapTap
{
    public class TextInputPanelUI : MonoBehaviour
    {

        #region Privat Serialize Fields

        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _applyButton;

        #endregion


        #region Events

        public event Action<string> OnConfirmingTextEvent;

        #endregion

        #region UI Callbacks

        public void OnInputFieldChanged(string text)
        {
            if (string.IsNullOrWhiteSpace(text) == _applyButton.interactable)
            {
                _applyButton.interactable = !_applyButton.interactable;
            }
        }

        public void OnApplied()
        {
            OnConfirmingTextEvent?.Invoke(_inputField.text);
        }

        public void SetPlaseholderText(string text)
        {
            _inputField.text = text;
        }

        #endregion
    }
}
