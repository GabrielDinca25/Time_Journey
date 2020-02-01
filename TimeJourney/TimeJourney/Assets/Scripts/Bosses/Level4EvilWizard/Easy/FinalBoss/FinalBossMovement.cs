using System.Collections;
using UnityEngine;

public class FinalBossMovement : MonoBehaviour
{
    // The general shot of the boss
    public GameObject shot;

    // The straight shot of the boss
    public GameObject shotStraight;

    // The diagonal shot of the boss
    public GameObject shotDiagonally;

    // The attack of the boss
    private IEnumerator attack;

    // Boolean indicating if the boss should attack normall
    public bool normal;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        transform.localScale = new Vector3(0.03f, 0.03f, 0);
        GetComponent<FinalBossEnterFromPortal>().enabled = true;
    }

    /// <summary>
    /// Chooses the boss attack
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
        for (int i = 0; i < 3; i++)
        {
            Instantiate(shot, transform.position, Quaternion.identity);
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
        for (int i = 0; i < 4; i++)
        {
            if (i % 2 == 0)
            {
                Instantiate(shotStraight, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(shotDiagonally, transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(1f);
        }

        GetComponent<FinalBossHealth>().SetBossColorState(true);
        Invoke("RetreatBoss", 2f);
    }

    /// <summary>
    /// Retreats the boss
    /// </summary>
    void RetreatBoss()
    {
        GetComponent<FinalBossHealth>().SetBossColorState(false);
        GetComponent<WizardBossRetreat>().enabled = true;
    }


}
