using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{

    [HideInInspector] public static GameSaveManager instance;

    public SaveSystemSO saveSystemSO;

    /// <summary>
    /// The method called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        Load();
    }

    /// <summary>
    /// Saves the state of the game
    /// </summary>
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerinfo.dat");

        PlayerInfo data = new PlayerInfo();

        data.m_PlayerPositionX = saveSystemSO.m_PlayerPositionX;
        data.m_PlayerPositionY = saveSystemSO.m_PlayerPositionY;
        data.m_SceneName = saveSystemSO.m_SceneName;
        data.m_Difficulty = saveSystemSO.m_Difficulty;

        bf.Serialize(file, data);
        file.Close();
    }

    /// <summary>
    /// Sets the saved data of the players
    /// </summary>
    /// <param name="posX">The player X coord</param>
    /// <param name="posY">The player T coord</param>
    /// <param name="sceneName">The scene name</param>
    /// <param name="difficulty">The difficulty chosen</param>
    public void SetSaveSystemPlayerData(float posX, float posY, string sceneName, string difficulty)
    {
        saveSystemSO.m_PlayerPositionX = posX;
        saveSystemSO.m_PlayerPositionY = posY;
        saveSystemSO.m_SceneName = sceneName;
        saveSystemSO.m_Difficulty = difficulty;
    }

    /// <summary>
    /// Loads the saved data
    /// </summary>
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerinfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerinfo.dat", FileMode.Open);

            PlayerInfo data = (PlayerInfo)bf.Deserialize(file);

            SetSaveSystemPlayerData(data.m_PlayerPositionX, data.m_PlayerPositionY, data.m_SceneName, data.m_Difficulty);

            file.Close();
        }
        else
        {
            saveSystemSO.m_PlayerPositionX = 0;
            saveSystemSO.m_PlayerPositionY = 0;
            saveSystemSO.m_SceneName = "IntroCity";
            saveSystemSO.m_Difficulty = "";
        }
    }
}

[System.Serializable]
class PlayerInfo
{
    public float m_PlayerPositionX;
    public float m_PlayerPositionY;
    public string m_SceneName;
    public string m_Difficulty;
}