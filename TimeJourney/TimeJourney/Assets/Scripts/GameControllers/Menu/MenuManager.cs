using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // The SaveSystemSO object
    public SaveSystemSO saveSystemSO;

    /// <summary>
    /// Selects the game difficulty
    /// </summary>
    /// <param name="type"></param>
    public void SelectDifficulty(string type)
    {
        saveSystemSO.m_Difficulty = type;
        StartNewGame();
    }

    /// <summary>
    /// Starts new game
    /// </summary>
    public void StartNewGame()
    {
        SceneManager.LoadScene("IntroCity");
    }

    /// <summary>
    /// Continues game at the last state
    /// </summary>
    public void ContinueGame()
    {
        saveSystemSO.m_LoadGame = true;
        SceneManager.LoadScene(saveSystemSO.m_SceneName + saveSystemSO.m_Difficulty);
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}

