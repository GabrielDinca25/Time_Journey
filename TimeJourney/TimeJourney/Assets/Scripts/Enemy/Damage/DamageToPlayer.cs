using System.Collections;
using UnityEngine;

public class DamageToPlayer : MonoBehaviour
{
    // The enemy damage amount
    public int m_enemyDamageAmount = 40;

    // The delay between attacks
    public float m_delayBetweenAttacks = 0.5f;

    // Check if should apply next dmg to player
    private bool dmg; 

    // The damage to the player
    private IEnumerator damageToPlayer;

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's collider
    /// </summary>
    /// <param name="other">The collider of the object that makes contact to the collider attached to this object</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
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
            GameController.instance.player.GetComponent<PlayerHealth>().GetDamage(m_enemyDamageAmount);
            yield return new WaitForSeconds(m_delayBetweenAttacks);
        }
    }

    /// <summary>
    /// Sent when a collider on another object stops touching this object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The collider of the object that stops touchings the collider attached to this object</param>
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopCoroutine(damageToPlayer);
        }
    }

}

