using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInMoveZone : MonoBehaviour
{
    // The enemy movement
    public EnemyMovement em;

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that leaves the trigger attached to this object</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy") && other.gameObject.transform.parent.Equals(transform.parent))
        {
            em.PlayerOutOfSight();
        }
    }
}
