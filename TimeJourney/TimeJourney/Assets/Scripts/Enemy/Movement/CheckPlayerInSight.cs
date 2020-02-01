using UnityEngine;

public class CheckPlayerInSight : MonoBehaviour
{
    // The enemy movement component
    private EnemyMovement em;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    private void Start()
    {
        em = GetComponentInParent<EnemyMovement>();
    }


    /// <summary>
    /// Sent when another object enters a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that enters the trigger attached to this object</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            em.PlayerInSight();
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that leaves the trigger attached to this object</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        em.PlayerOutOfSight();
    }
}
