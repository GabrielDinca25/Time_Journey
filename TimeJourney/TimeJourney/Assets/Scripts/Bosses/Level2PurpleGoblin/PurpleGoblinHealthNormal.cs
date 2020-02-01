using System.Collections;
using UnityEngine;

public class PurpleGoblinHealthNormal : Health
{
    // The damage animation
    private IEnumerator damageAnimation;

    // The body parts of the Sprite Renderer
    private SpriteRenderer bodyParts;

    // The manager of the boss fight
    public TriggerBossFight triggerBossFight;

    // The position where the goblin is spawned
    private Vector3 spawnPosition;

    // Bool indicating if the goblin receives damage
    public bool receiveDMG;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        m_CurrentHealth = m_maxHp;
        transform.position = spawnPosition;
        receiveDMG = false;
    }

    /// <summary>
    /// The method called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        spawnPosition = transform.position;
    }

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    public override void Start()
    {
        base.Start();

        bodyParts = GetComponentInChildren<SpriteRenderer>();
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that enters the trigger attached to this object</param>
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Water"))
        {
            receiveDMG = true;
            GetComponentInChildren<SpriteRenderer>().color = new Color(1, 0, 0.3f);
            Invoke("DisableDamage", 3f);
        }
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
        DisableDamage();
        CancelInvoke("DisableDamage");
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
        DisableDamage();
        CancelInvoke("DisableDamage");
    }


    public void DisableDamage()
    {
        receiveDMG = false;
        GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1f);
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
        bodyParts.enabled = true;
    }

    /// <summary>
    /// `The Damage animation, makes the Goblin blink
    /// </summary>
    /// <returns>IEnumarator for the interval of the blinks</returns>
    public IEnumerator DamageAnimation()
    {
        for (int i = 0; i < 12; i++)
        {
            bodyParts.enabled = !bodyParts.enabled;
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
