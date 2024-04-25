using System.IO;
using UnityEngine;


namespace AlexDev.DataModule
{

    public class DataManager : MonoBehaviour
    {
        public static DataManager instance;

        #region PublicFields

        public GameSettingsData gameSettings { get; private set; }

        #endregion

        #region Private Fields

        private const string GAME_SETTINGS_FILE_NAME = "GameSettings";

        #endregion

        #region Monobehaviour Callbacks

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGameSettings();
        }

        #endregion

        #region Public Methods

        public void SaveGameSettings()
        {
            SaveData(GAME_SETTINGS_FILE_NAME, gameSettings);
        }


        public void LoadGameSettings()
        {
            if (TryLoadData<GameSettingsData>(GAME_SETTINGS_FILE_NAME, out var loadedData))
            {
                gameSettings = loadedData;
            }
            else
            {
                gameSettings = new GameSettingsData();
            }
        }

        public void SaveData<T>(T data) where T : class
        {
            SaveData(typeof(T).ToString(), data);
        }

        public bool TryLoadData<T>(out T dataObject) where T : class
        {
            return TryLoadData<T>(typeof(T).ToString(), out dataObject);
        }

        #endregion

        #region Private Methods

        private void SaveData(string saveFileName, object dataObject)
        {
            string json = JsonUtility.ToJson(dataObject);
            File.WriteAllText(Application.persistentDataPath + $"/{saveFileName}.json", json);
        }

        private bool TryLoadData<T>(string fileName, out T dataObject) where T : class
        {
            dataObject = LoadData<T>(fileName);
            if (dataObject != null)
            {
                return true;
            }
            return false;
        }


        private T LoadData<T>(string saveFileName) where T : class
        {
            T loadedData;
            string path = Application.persistentDataPath + $"/{saveFileName}.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                loadedData = JsonUtility.FromJson<T>(json);
            }
            else
            {
                loadedData = null;
            }
            return loadedData;
        }

        #endregion



    }


}
