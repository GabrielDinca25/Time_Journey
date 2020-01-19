using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject player;
    public GameObject swordLogic;
    public SaveSystemSO saveSystemSO;
    private bool m_death;

    public Action SpecialAction = delegate { };
    // Menu
    GameObject m_Menu;

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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene("Menu");
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            saveSystemSO.m_LoadGame = true;
            LoadGame();
        }

    }

    public void GameOver()
    {
        if (!m_death)
        {
            m_death = true;

            Time.timeScale = 0;

            Revive();
        }
    }

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

    public void LoadGame()
    {
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

    public void DoSpecialAction()
    {
        SpecialAction();
    }

}