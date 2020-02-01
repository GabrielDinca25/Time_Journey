using UnityEngine;

public class CheckContinueButton : MonoBehaviour
{
    public SaveSystemSO saveSystemSO;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    private void Start()
    {
        if (saveSystemSO.m_Difficulty.Length == 0 && saveSystemSO.m_SceneName == "IntroCity")
        {
            //Disables the gameObject
            gameObject.SetActive(false);
        }
    }
}
