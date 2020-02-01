using System.Collections;
using UnityEngine;

public class GoblinBossHealth : Health
{
    // The damage animations
    private IEnumerator damageAnimation;

    // The body parts of the sprite renderer
    public SpriteRenderer[] bodyParts;

    // The manager of the boss fight
    public TriggerBossFight triggerBossFight;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        transform.localScale = new Vector3(0.003f, 0.003f, 0);
        m_CurrentHealth = m_maxHp;
        GetComponent<GoblinBossEnter>().enabled = true;
    }

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    public override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Deals damage to goblin
    /// </summary>
    /// <param name="dmgAmount">The damage amount</param>
    public override void GetDamage(int dmgAmount)
    {
        m_CurrentHealth -= dmgAmount;
        if (m_CurrentHealth <= 0)
        {
            Die();
            return;
        }
        GetDamageAnimation();
    }

    /// <summary>
    /// Deals damage to goblin according to the attack type
    /// </summary>
    /// <param name="type">The type of the attack</param>
    /// <param name="dmgAmount">The damage amount</param>
    public override void GetDamage(string type, int dmgAmount)
    {
        m_CurrentHealth -= dmgAmount;
        if (m_CurrentHealth <= 0)
        {
            Die();
            return;
        }
        GetDamageAnimation();
    }

    /// <summary>
    /// Gets and starts damage animation
    /// </summary>
    public override void GetDamageAnimation()
    {
        damageAnimation = DamageAnimation();
        PrepareNextDamageAnimation();
        StartCoroutine(damageAnimation);
    }

    /// <summary>
    /// Prepares next damage animation
    /// </summary>
    public void PrepareNextDamageAnimation()
    {
        StopAllCoroutines();
        for (int j = 0; j < bodyParts.Length; j++)
        {
            bodyParts[j].enabled = true;
        }
    }

    /// <summary>
    /// `The Damage animation, makes the Goblin blink
    /// </summary>
    /// <returns>IEnumarator for the interval of the blinks</returns>
    public IEnumerator DamageAnimation()
    {
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < bodyParts.Length; j++)
            {
                bodyParts[j].enabled = !bodyParts[j].enabled;
            }
            yield return new WaitForSeconds(.1f);
        }
    }

    /// <summary>
    /// Kills the goblin
    /// </summary>
    public override void Die()
    {
        triggerBossFight.Revert();
    }
}
