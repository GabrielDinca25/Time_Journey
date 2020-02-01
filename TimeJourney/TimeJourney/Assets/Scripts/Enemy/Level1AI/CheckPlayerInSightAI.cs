using UnityEngine;

public class CheckPlayerInSightAI : MonoBehaviour
{
    // The movement of the enemy AI
    private EnemyAIMovement em;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    private void Start()
    {
        em = GetComponentInParent<EnemyAIMovement>();
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that enters the trigger attached to this object</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.name);
            em.patrolling = false;
        }
    }
}
