using System;
using UnityEngine;

namespace AlexDev.LapTap
{
    public class NewGamePanelUI : MonoBehaviour
    {
        #region Private Fields

        private int columns;
        private int rows;

        #endregion

        #region Events

        public event Action<int, int> StartGamePressedEvent;

        #endregion

        #region Publick Methods

        public void SetRows(int columnsOptionValue)
        {
            columns = (columnsOptionValue + 2) * 2;
        }

        public void SetColumns(int rowsOptionValue)
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
