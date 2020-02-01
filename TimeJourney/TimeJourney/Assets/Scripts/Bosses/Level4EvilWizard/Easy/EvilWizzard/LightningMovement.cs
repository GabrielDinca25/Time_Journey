using System.Collections;
using UnityEngine;

public class LightningMovement : MonoBehaviour
{
    // The possible positions of the lihting
    public Vector3[] position;

    // The position to instantiate the lighting
    Vector3 goPosition;

    // The boss gameobject
    public GameObject boss;

    // The enemy damage amount
    public int m_enemyDamageAmount = 40;

    // The delay between attacks
    public float m_delayBetweenAttacks = 0.5f;

    private IEnumerator damageToPlayer;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        transform.position = position[Random.Range(0, position.Length)];
        if (transform.position == position[0])
        {
            goPosition = position[1];
        }
        else
        {
            goPosition = position[0];
        }

    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, goPosition, 2f * Time.deltaTime);

        if (transform.position == goPosition)
        {
            //Disables the gameObject
            gameObject.SetActive(false);
        }
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
            //Disables the gameObject
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// The Damage animation, makes the wizard blink
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

    /// <summary>
    /// The function called when the behaviour becomes disabled.
    /// </summary>
    void OnDisable()
    {
        boss.GetComponent<EvilWizardHealth>().SetBossColorState(true);

        Invoke("RetreatBoss", 2f);
    }

    /// <summary>
    /// Retreats the boss
    /// </summary>
    void RetreatBoss()
    {
        boss.GetComponent<EvilWizardHealth>().SetBossColorState(false);
        boss.GetComponent<WizardBossRetreat>().enabled = true;
    }
}
