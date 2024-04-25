using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AlexDev.LapTap
{
    public class NewGamePanelUI : MonoBehaviour
    {
        #region Serialize Private Feilds

        [SerializeField] private TMP_Dropdown _columnsDropdown;
        [SerializeField] private TMP_Dropdown _rowsDropdown;

        #endregion

        #region Private Fields

        private int columns;
        private int rows;

        #endregion

        #region Events

        public event Action<int, int> StartGamePressedEvent;

        #endregion

        #region MonoBehaviour Callbacks

        private void Start()
        {
            SetColumns(_columnsDropdown.value);
            SetRows(_rowsDropdown.value);
        }

        #endregion

        #region Publick Methods

        public void SetColumns(int columnsOptionValue)
        {
            columns = (columnsOptionValue + 1) * 2;
        }

        public void SetRows(int rowsOptionValue)
        {
            rows = rowsOptionValue + 2;
        }

        public void OnStartButtonPresed()
        {
            StartGamePressedEvent?.Invoke(columns, rows);
        }

        #endregion
    }
}
