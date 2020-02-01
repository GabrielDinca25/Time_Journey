using UnityEngine;

public class TriggerBossFightWizardNormal : MonoBehaviour
{
    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    private void Start()
    {
        GameController.instance.SpecialAction = GetComponent<SpecialAction>().DoSpecialAction;
    }
}
