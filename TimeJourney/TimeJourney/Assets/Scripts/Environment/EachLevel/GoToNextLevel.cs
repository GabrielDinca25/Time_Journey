using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextLevel : MonoBehaviour
{
    // The next scene string
    public string m_nextSceneName;

    // The save system SO instance
    public SaveSystemSO saveSystemSO;

    // The fade effect gameObject
    public GameObject fade;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    void Start()
    {
        m_nextSceneName += saveSystemSO.m_Difficulty;
    }

    /// <summary>
    /// Changes scene to next scene
    /// </summary>
    public void ChangeScene()
    {
        StartCoroutine(LoadScene(m_nextSceneName));
    }

    /// <summary>
    /// Loads scene to the specified level
    /// </summary>
    /// <param name="Level">The level to change to</param>
    /// <returns></returns>
    IEnumerator LoadScene(string Level)
    {
        fade.SetActive(true);
        fade.GetComponent<Animator>().SetTrigger("end");
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(Level);
    }
}
