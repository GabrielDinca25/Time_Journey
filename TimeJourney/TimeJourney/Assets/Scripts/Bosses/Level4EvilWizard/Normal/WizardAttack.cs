using System.Collections;
using UnityEngine;

public class WizardAttack : MonoBehaviour
{

    // The lightning gameobject
    public GameObject lightning;

    // The lightning attack
    private IEnumerator lightningAttack;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    void Start()
    {
        Invoke("InitiateAttack", 5f);
    }

    /// <summary>
    /// Initiates the wizard attack
    /// </summary>
    public void InitiateAttack()
    {
        GetComponent<WizardMovement>().attack = true;
        GetComponent<EvilWizardNormalHealth>().SetBossColorState(true);
        lightning.GetComponent<LightningMovementNormal>().startPosition = GameController.instance.player.transform.position;
        Invoke("LaunchAttack", 2f);
    }

    /// <summary>
    /// Launches the attack
    /// </summary>
    public void LaunchAttack()
    {
        lightning.SetActive(true);
    }

    /// <summary>
    /// Stops the attack
    /// </summary>
    public void StopAttack()
    {
        GetComponent<EvilWizardNormalHealth>().SetBossColorState(false);
        GetComponent<WizardMovement>().attack = false;
        if (GetComponent<EvilWizardNormalHealth>().m_CurrentHealth > 0)
        {
            Invoke("InitiateAttack", 5f);
        }
    }

    /// <summary>
    /// The function called when the behaviour becomes disabled.
    /// </summary>
    private void OnDisable()
    {
        CancelInvoke("InitiateAttack");
        CancelInvoke("LaunchAttack");
    }
}
