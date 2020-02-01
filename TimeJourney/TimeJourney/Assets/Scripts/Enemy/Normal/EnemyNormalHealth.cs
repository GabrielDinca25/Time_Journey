using System.Collections;
using UnityEngine;

public class EnemyNormalHealth : Health
{
    //[HideInInspector] public Animator m_animator;
    
    // The damage animation
    private IEnumerator damageAnimation;

    // The particle system of the enemy
    private ParticleSystem body;

    //Boolean indicate if enemy is weak to fire
    public bool weakAtFire;
    //Boolean indicate if enemy is imune to fire
    public bool ImuneAtFire;
    //Boolean indicate if enemy is weak to ice
    public bool weakAtIce;
    //Boolean indicate if enemy is imune to ice
    public bool ImuneAtIce;
    //Boolean indicate if enemy is weak to light
    public bool weakAtLight;
    //Boolean indicate if enemy is imune to light
    public bool ImuneAtLight;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
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

    /// <summary>
    /// Gets and starts damage animation
    /// </summary>
    public override void GetDamageAnimation()
    {
        PrepareNextDamageAnimation();
        damageAnimation = DamageAnimation();
        StartCoroutine(damageAnimation);
    }

    /// <summary>
    /// Prepares next damage animation
    /// </summary>
    public void PrepareNextDamageAnimation()
    {
        StopAllCoroutines();
    }


    /// <summary>
    /// `The Damage animation, makes the Goblin blink
    /// </summary>
    /// <returns>IEnumarator for the interval of the blinks</returns>
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








