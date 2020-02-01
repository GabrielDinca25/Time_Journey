using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // The next scene name
    public string m_NextSceneName;

    // The save system SO instance
    public SaveSystemSO saveSystemSO;

    // The fade effect gameObject
    public GameObject fade;

    // Bool indicating if non level scene must be switched
    public bool switchToNonLevelScene;

    public bool onTrigger;

    public void Start()
    {
        onTrigger = false;
        if (!switchToNonLevelScene)
        {
            m_NextSceneName += saveSystemSO.m_Difficulty;
        }
    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && onTrigger)
        {
            ChangeScene();
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that enters the trigger attached to this object</param>
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onTrigger = true;
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that leaves the trigger attached to this object</param>
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onTrigger = false;
        }
    }
    public void ChangeScene()
    {
        StartCoroutine(LoadScene(m_NextSceneName));
    }

    IEnumerator LoadScene(string Level)
    {
        Destroy(GameController.instance);
        fade.SetActive(true);
        fade.GetComponent<Animator>().SetTrigger("end");
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(Level);
    }
}