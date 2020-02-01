using System.Collections;
using UnityEngine;

public class FinalBossMovementV2 : MonoBehaviour
{
    // The damage portal
    public GameObject HurtingPortal;

    // The positions of the portal
    public Vector3[] positions;

    // The shot of the boss
    public GameObject shot;

    // The attack of the boss
    private IEnumerator attack;

    // Boolean indicating if the boss attack should be normal
    public bool normal;

    // Boolean indicating if the boss is freezed 
    public bool freezed;

    // The duration of the freeze time
    public float timeFreezed;

    // The body parts of the sprite renderer
    public SpriteRenderer[] bodyParts;

    // Tutorial text
    public GameObject tutorialText;

    // Boolean that indicates one time action
    private bool once;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        if (!once)
        {
            once = true;
            return;
        }

        if (normal)
        {
            tutorialText.SetActive(true);
        }

        transform.localScale = new Vector3(0.03f, 0.03f, 0);
        GetComponent<FinalBossEnterFromPortal>().enabled = true;
    }

    /// <summary>
    /// Chooses the attack of the boss
    /// </summary>
    public void Attack()
    {
        if (normal)
        {
            attack = StartAttackNormal();
        }
        else
        {
            attack = StartAttack();
        }
        StartCoroutine(attack);
    }

    /// <summary>
    /// Starts the boss attack
    /// </summary>
    /// <returns></returns>
    IEnumerator StartAttack()
    {
        for (int i = 0; i < 5; i++)
        {
            int random = Random.Range(0, positions.Length);
            Instantiate(HurtingPortal, positions[random], Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
        GetComponent<FinalBossHealth>().SetBossColorState(true);
        Invoke("RetreatBoss", 2f);
    }

    /// <summary>
    /// Starts the boss normal attack
    /// </summary>
    /// <returns></returns>
    IEnumerator StartAttackNormal()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(shot, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }

        if (!freezed)
        {
            Retreat();
        }
    }
    
    /// <summary>
    /// Retreats the boss
    /// </summary>
    void RetreatBoss()
    {
        GetComponent<FinalBossHealth>().SetBossColorState(false);
        GetComponent<WizardBossRetreat>().enabled = true;
    }

    /// <summary>
    /// Changes color of boss
    /// </summary>
    void Retreat()
    {
        CancelInvoke();
        for (int j = 0; j < bodyParts.Length; j++)
        {
            bodyParts[j].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        }
        GetComponent<WizardBossRetreat>().enabled = true;
    }

    /// <summary>
    /// Freezes the boss
    /// </summary>
    public void Freeze()
    {
        for (int j = 0; j < bodyParts.Length; j++)
        {
            bodyParts[j].GetComponent<SpriteRenderer>().color = new Color(0, 1, 1);
        }
        freezed = true;
        GetComponent<FinalBossHealth>().receiveDMG = true;
        Invoke("DisableFreeze", timeFreezed);
    }

    /// <summary>
    /// Unfreezes the boss
    /// </summary>
    public void DisableFreeze()
    {
        freezed = false;
        GetComponent<FinalBossHealth>().receiveDMG = false;
        Retreat();

    }
}
