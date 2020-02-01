using UnityEngine;

public class ActivateIceByTrigger : MonoBehaviour
{
    /// <summary>
    /// Sent when an incoming collider makes contact with this object's collider
    /// </summary>
    /// <param name="other">The collider of the object that makes contact to the collider attached to this object</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
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
            //Disables the gameObject
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
