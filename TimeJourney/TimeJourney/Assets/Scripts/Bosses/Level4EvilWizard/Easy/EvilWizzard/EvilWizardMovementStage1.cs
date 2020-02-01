using UnityEngine;

public class EvilWizardMovementStage1 : MonoBehaviour
{
    // The portal gameobject that the wizard instantiates
    public GameObject wizzardPortal;

    // The lighting gameobject that the wizard instantiates
    public GameObject lightning;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        transform.localScale = new Vector3(0.03f, 0.03f, 0);
        GetComponent<WizardBossEnter>().enabled = true;
    }

    /// <summary>
    /// Activates the lightning attack
    /// </summary>
    public void Attack()
    {
        lightning.SetActive(true);
    }
}
