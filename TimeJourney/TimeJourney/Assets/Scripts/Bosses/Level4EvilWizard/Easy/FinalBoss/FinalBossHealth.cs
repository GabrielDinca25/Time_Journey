﻿using System.Collections;
using UnityEngine;

public class FinalBossHealth : Health
{
    // The damage animation 
    private IEnumerator damageAnimation;

    // The body parts of the sprite renderer
    public SpriteRenderer[] bodyParts;

    // The manager of the boss fight
    public TriggerBossFightWizard triggerBossFight;

    // Boolean indicating if the boss should receive damage
    public bool receiveDMG;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        PrepareNextDamageAnimation();
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
        if (!receiveDMG)
        {
            return;
        }
        m_CurrentHealth -= dmgAmount;
        if (m_CurrentHealth <= 0)
        {
            Die();
            return;
        }
        GetDamageAnimation();
        SetBossColorState(false);
    }

    /// <summary>
    /// Deals damage to goblin according to the attack type
    /// </summary>
    /// <param name="type">The type of the attack</param>
    /// <param name="dmgAmount">The damage amount</param>
    public override void GetDamage(string type, int dmgAmount)
    {
        if (!receiveDMG)
        {
            return;
        }
        m_CurrentHealth -= dmgAmount;

        if (m_CurrentHealth <= 0)
        {
            Die();
            return;
        }
        GetDamageAnimation();
        SetBossColorState(false);

    }

    /// <summary>
    /// Sets the color state of the wizard
    /// </summary>
    /// <param name="canReceiveDamage">Boolean indicating if the boss in the receive damage mode</param>
    public void SetBossColorState(bool canReceiveDamage)
    {
        Color newColor;
        if (canReceiveDamage)
        {
            newColor = new Color(1, 0, 0.3f);
            receiveDMG = true;
        }
        else
        {
            newColor = new Color(0, 0, 0);
            receiveDMG = false;
        }
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].enabled = true;
            bodyParts[i].GetComponent<SpriteRenderer>().color = newColor;
        }
    }

    /// <summary>
    /// Gets and starts damage animation
    /// </summary>
    public override void GetDamageAnimation()
    {
        damageAnimation = DamageAnimation();
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
        PrepareNextDamageAnimation();
    }

    /// <summary>
    /// Kills the boss
    /// </summary>
    public override void Die()
    {
        triggerBossFight.Revert();
    }

}
