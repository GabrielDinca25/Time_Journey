using System.Collections;
using UnityEngine;

public class PurpleGoblinHealth : Health
{
    // The damage animation
    private IEnumerator damageAnimation;

    // The body parts of the Sprite Renderer
    private SpriteRenderer bodyParts;

    // The manager of the boss fight
    public TriggerBossFight triggerBossFight;

    // The position where the goblin is spawned
    private Vector3 spawnPosition;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        m_CurrentHealth = m_maxHp;
        transform.position = spawnPosition;
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
        // If the goblin is hit by a pickable red box, deal damage
        if (other.name.Contains("Pickable") && other.gameObject.GetComponent<SpriteRenderer>().sprite.name.Equals("RedBox"))
        {
            //Disables the gameObject
            other.gameObject.SetActive(false);
            m_CurrentHealth -= 10;
            if (m_CurrentHealth <= 0)
            {
                Die();
                return;
            }
            GetDamageAnimation();
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
