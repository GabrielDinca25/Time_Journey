using System.Collections;
using UnityEngine;

public class PortalHurting : MonoBehaviour
{
    // The portal animation
    private IEnumerator portalAnimation;

    // The renderer of the portal particle system
    private ParticleSystemRenderer portalRenderer;

    // The circle collider of the portal
    private CircleCollider2D cc2D;

    // The enemy damage amount
    public int m_enemyDamageAmount = 20;
    
    // The delay between attacks 
    public float m_delayBetweenAttacks = 0.5f;

    // check if should apply next damage to player
    private bool dmg; 

    private IEnumerator damageToPlayer;

    /// <summary>
    /// The method called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        portalRenderer = GetComponentInChildren<ParticleSystem>().GetComponent<ParticleSystemRenderer>();
        cc2D = GetComponent<CircleCollider2D>();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        portalAnimation = PortalIncreaseAnimation();
        StartCoroutine(portalAnimation);
    }

    /// <summary>
    /// Increases the portal size
    /// </summary>
    /// <returns></returns>
    public IEnumerator PortalIncreaseAnimation()
    {
        while (portalRenderer.minParticleSize < 0.3f)
        {
            portalRenderer.minParticleSize += 0.01f;
            portalRenderer.maxParticleSize = portalRenderer.minParticleSize;

            cc2D.radius += 0.025f;
            yield return new WaitForSeconds(.1f);
        }

        portalAnimation = PortalDecreaseAnimation();
        StartCoroutine(portalAnimation);
    }

    /// <summary>
    /// Decreases the portal size
    /// </summary>
    /// <returns></returns>
    public IEnumerator PortalDecreaseAnimation()
    {
        while (portalRenderer.maxParticleSize > 0)
        {
            portalRenderer.maxParticleSize -= 0.01f;
            portalRenderer.minParticleSize = portalRenderer.maxParticleSize;
            cc2D.radius -= 0.025f;
            yield return new WaitForSeconds(.1f);
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that enters the trigger attached to this object</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            damageToPlayer = DamageAnimation();
            StartCoroutine(damageToPlayer);
        }
    }

    /// <summary>
    /// `The Damage animation, makes the Goblin blink
    /// </summary>
    /// <returns>IEnumarator for the interval of the blinks</returns>
    public IEnumerator DamageAnimation()
    {
        while (true)
        {
            GameController.instance.player.GetComponent<PlayerHealth>().GetDamage(m_enemyDamageAmount, true);
            yield return new WaitForSeconds(m_delayBetweenAttacks);
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that leaves the trigger attached to this object</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(damageToPlayer);
        }
    }
}
