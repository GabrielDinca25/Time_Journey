using UnityEngine;

public class PlayerHold : MonoBehaviour
{
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that enters the trigger attached to this object</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent.parent.SetParent(gameObject.transform);
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
            other.transform.parent.parent.parent = null;
        }
    }
}
