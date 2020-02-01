using UnityEngine;

public class ShowSelectDifficulty : MonoBehaviour
{
    // The pop-up gameObject to show
    public GameObject toShow;

    /// <summary>
    /// Enables the difficulty select pop-up
    /// </summary>
    public void ShowDifficultySelect()
    {
        toShow.SetActive(true);
    }

    /// <summary>
    /// Hides the difficulty select pop-up
    /// </summary>
    public void hideDifficultySelect()
    {
        toShow.SetActive(false);
    }
}
