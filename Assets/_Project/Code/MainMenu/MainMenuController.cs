using UnityEngine;
using AlexDev.DataModule;
using UnityEngine.SceneManagement;
using AlexDev.CatchMe.Audio;

namespace AlexDev.LapTap
{
    public class MainMenuController : MonoBehaviour
    {
        #region Serialize Private FIelds

        [SerializeField] private MenuUI _menuUI;

        #endregion

        #region Private Fields

        private DataManager _dataManager;

        private AudioManager _audioManager;

        private GameData _gameData;

        #endregion

        void Start()
        {
            _dataManager = DataManager.instance;
            _audioManager = AudioManager.instance;

            _audioManager.PlayMusic("MN");

            _menuUI.ContinueButtonPressedEvent += StartSavedGame;
            _menuUI.StartButtonPressedEvent += StartNewGame;
            _menuUI.ExitButtonPressedEvent += ExitGame;

            if (_dataManager.TryLoadData(out _gameData))
            {
                if (_gameData.hasSavedGame)
                {
                    _menuUI.EnableContinueButton();
                }
            }
            else
            {
                _gameData = new GameData();
            }
        }

        #region Private Methods

        private void StartNewGame(int columns, int rows)
        {
            var game = new GameData()
            {
                hiScore = _gameData.hiScore,
                recordHolderName = _gameData.recordHolderName,
                columns = columns,
                rows = rows
            };
            _dataManager.SaveData(game);
            StartSavedGame();
        }

        private void StartSavedGame()
        {
            SceneManager.LoadScene(1);
        }


        private void ExitGame()
        {
            Application.Quit();
        }

        #endregion


    }
}
