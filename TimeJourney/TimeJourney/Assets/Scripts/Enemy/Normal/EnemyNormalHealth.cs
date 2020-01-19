using System.Collections;
using UnityEngine;

public class EnemyNormalHealth : Health
{
    //[HideInInspector] public Animator m_animator;

    private IEnumerator damageAnimation;
    private ParticleSystem body;

    public bool weakAtFire;
    public bool ImuneAtFire;
    public bool weakAtIce;
    public bool ImuneAtIce;
    public bool weakAtLight;
    public bool ImuneAtLight;


    public override void Start()
    {
        base.Start();
        body = GetComponentInChildren<ParticleSystem>();
    }


    public override void GetDamage(int dmgAmount)
    {
        m_CurrentHealth -= dmgAmount;
        if (m_CurrentHealth <= 0)
        {
            Die();
            return;
        }
        GetComponent<EnemyNormalMovement>().PlayerInSight();
        GetDamageAnimation();
    }

    public void CheckHealthAndTriggerDeath()
    {
        if (m_CurrentHealth <= 0)
        {
            Die();
            return;
        }
    }

    public override void GetDamage(string type, int dmgAmount)
    {
        switch (type)
        {
            case "Fire":
                if (ImuneAtFire)
                {
                    return;
                }

                m_CurrentHealth -= weakAtFire ? dmgAmount * 3 : dmgAmount;

                break;
            case "Ice":
                if (ImuneAtIce)
                {
                    return;
                }

                m_CurrentHealth -= weakAtIce ? dmgAmount * 3 : dmgAmount;
                break;
            default:
                m_CurrentHealth -= dmgAmount;
                break;
        }

        CheckHealthAndTriggerDeath();
        GetComponent<EnemyNormalMovement>().PlayerInSight();
        GetDamageAnimation();
    }

    public override void Die()
    {
        Destroy(transform.parent.gameObject);
    }

    public override void GetDamageAnimation()
    {
        PrepareNextDamageAnimation();
        damageAnimation = DamageAnimation();
        StartCoroutine(damageAnimation);
    }

    public void PrepareNextDamageAnimation()
    {
        StopAllCoroutines();
    }

    public IEnumerator DamageAnimation()
    {
        for (int i = 0; i < 13; i++)
        {
            if (body.isPlaying)
            {
                body.Stop();
            }
            else
            {
                body.Play();
            }
            yield return new WaitForSeconds(.1f);

        }
        body.Play();
    }

}








