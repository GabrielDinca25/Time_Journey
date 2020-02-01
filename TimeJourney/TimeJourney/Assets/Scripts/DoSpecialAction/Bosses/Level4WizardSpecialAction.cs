using UnityEngine.SceneManagement;

public class Level4WizardSpecialAction : SpecialAction
{
    /// <summary>
    /// Does special action
    /// </summary>
    public override void DoSpecialAction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
