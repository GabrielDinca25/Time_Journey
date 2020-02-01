using System.Collections;
using UnityEngine;

public class LightningMovementNormal : MonoBehaviour
{
    // The start position of the lightning
    [HideInInspector] public Vector3 startPosition;

    // The wizard attack
    public WizardAttack wizardAttack;

    // The enemy damage amount
    public int m_enemyDamageAmount = 40;

    // The delay between attacks
    public float m_delayBetweenAttacks = 0.5f;

    // The damaget done to player
    private IEnumerator damageToPlayer;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        transform.position = startPosition;
        Invoke("Disable", 2f);
    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, GameController.instance.player.transform.position, 1f * Time.deltaTime);
    }

    private void Disable()
    {
        wizardAttack.StopAttack();
        //Disables the gameObject

        gameObject.SetActive(false);
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that enters the trigger attached to this object</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            damageToPlayer = DamageAnimation();
            StartCoroutine(damageToPlayer);
        }
        else if (other.name.Contains("Pickable") && other.gameObject.GetComponent<SpriteRenderer>().sprite.name.Equals("RedBox"))
        {
            gameObject.SetActive(false);
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
