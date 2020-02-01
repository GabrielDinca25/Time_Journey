using System.Collections;
using UnityEngine;

public class EvilWizardNormalHealth : Health
{
    // The damage animation
    private IEnumerator damageAnimation;

    // The body parts of the sprite renderer
    public SpriteRenderer[] bodyParts;

    // Bool indicating if the wizard should receive damage 
    public bool receiveDMG;

    // The collider of the whole scene
    public GameObject sceneCollider;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    public override void Start()
    {
        base.Start();
    }

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
            newColor = new Color(1, 1, 1);
            receiveDMG = false;
        }

        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].GetComponent<SpriteRenderer>().color = newColor;
        }
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

    public override void Die()
    {
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        }

        Invoke("Disable", 1f);

    }

    void Disable()
    {
        //Disables the gameObject
        gameObject.SetActive(false);
        sceneCollider.SetActive(false);
    }
}
