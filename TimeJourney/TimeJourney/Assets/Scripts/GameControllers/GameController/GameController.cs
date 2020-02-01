using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Game controller singleton
    public static GameController instance;

    // The player gameObject
    public GameObject player;

    // The swordLogic gameObject
    public GameObject swordLogic;

    // The save system gameObject
    public SaveSystemSO saveSystemSO;

    // Boolean indicating if the game should be over
    private bool m_death;

    // Custom special action to be done 
    public Action SpecialAction = delegate { };

    // The menu gameObject
    GameObject m_Menu;

    /// <summary>
    /// The method called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        LoadGame();
    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    public void Update()
    {
        // Check key inputs
        if (Input.GetKeyDown(KeyCode.F1))
        {
            // Go to main menu
            SceneManager.LoadScene("Menu");
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            // Load active scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Go to main menu
            SceneManager.LoadScene("Menu");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            //Load saved game
            saveSystemSO.m_LoadGame = true;
            LoadGame();
        }
    }
    
    /// <summary>
    /// Ends the game
    /// </summary>
    public void GameOver()
    {
        if (!m_death)
        {
            m_death = true;

            // Stop the time
            Time.timeScale = 0;

            // Revive player
            Revive();
        }
    }

    /// <summary>
    /// Revives the player
    /// </summary>
    private void Revive()
    {
        string currentSceneName = saveSystemSO.m_SceneName + saveSystemSO.m_Difficulty;

        if (currentSceneName.Equals(SceneManager.GetActiveScene().name))
        {
            GetComponent<RevivePlayer>().Revive();
        }
        else
        {
            player.transform.position = Vector3.zero;
        }
        m_death = false;
        Time.timeScale = 1;
        player.GetComponent<PlayerHealth>().Revive();
    }

    /// <summary>
    /// Saves the game
    /// </summary>
    public void SaveGame()
    {
        saveSystemSO.m_PlayerPositionX = player.transform.position.x;
        saveSystemSO.m_PlayerPositionY = player.transform.position.y;
        //save current level name without the m_difficulty
        if (saveSystemSO.m_Difficulty.Contains("Easy"))
        {
            saveSystemSO.m_SceneName = SceneManager.GetActiveScene().name.Replace("Easy", "");
        }
        else if (saveSystemSO.m_Difficulty.Contains("Normal"))
        {
            saveSystemSO.m_SceneName = SceneManager.GetActiveScene().name.Replace("Normal", "");
        }

        if (GameSaveManager.instance != null)
        {
            GameSaveManager.instance.Save();
        }
    }

    /// <summary>
    /// Loads the game
    /// </summary>
    public void LoadGame()
    {
        // Check if the current scene name is not the active scene
        string currentSceneName = saveSystemSO.m_SceneName + saveSystemSO.m_Difficulty;
        if (!currentSceneName.Equals(SceneManager.GetActiveScene().name))
        {
            if (SceneManager.GetActiveScene().name.Contains("Level1"))
            {
                if (player.transform.position.x == -13.85f)
                {
                    SaveLevel1();
                    return;
                }
            }
            SaveGame();
        }
        if (!saveSystemSO.m_LoadGame)
        {
            return;
        }
        ///safe check for lvl1 
        if (SceneManager.GetActiveScene().name.Contains("Level1"))
        {
            if (player.transform.position.x == -13.85f)
            {
                saveSystemSO.m_LoadGame = false;
                return;
            }
        }
        player.transform.position = new Vector3(saveSystemSO.m_PlayerPositionX,
                                               saveSystemSO.m_PlayerPositionY,
                                               0);
        saveSystemSO.m_LoadGame = false;
    }

    /// <summary>
    /// Saves Level 1
    /// </summary>
    public void SaveLevel1()
    {
        saveSystemSO.m_PlayerPositionX = 0;
        saveSystemSO.m_PlayerPositionY = 0;
        //save current level name without the m_difficulty

        if (saveSystemSO.m_SceneName.Equals("IntroCity"))
        {
            if (SceneManager.GetActiveScene().name.Contains("Easy"))
            {
                saveSystemSO.m_SceneName = SceneManager.GetActiveScene().name.Replace("Easy", "");
            }
            else if (SceneManager.GetActiveScene().name.Contains("Normal"))
            {
                saveSystemSO.m_SceneName = SceneManager.GetActiveScene().name.Replace("Normal", "");
            }
        }
        else
        {
            if (saveSystemSO.m_SceneName.Contains("Easy"))
            {
                saveSystemSO.m_SceneName = SceneManager.GetActiveScene().name.Replace("Easy", "");
            }
            else if (saveSystemSO.m_SceneName.Contains("Normal"))
            {
                saveSystemSO.m_SceneName = SceneManager.GetActiveScene().name.Replace("Normal", "");
            }
        }

        if (GameSaveManager.instance != null)
        {
            GameSaveManager.instance.Save();
        }
    }

    /// <summary>
    /// Does special action
    /// </summary>
    public void DoSpecialAction()
    {
        SpecialAction();
    }

}