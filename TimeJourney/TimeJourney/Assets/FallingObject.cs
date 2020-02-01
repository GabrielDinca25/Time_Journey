using UnityEngine;

public class FallingObject : FallingPlatform
{
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that enters the trigger attached to this object</param>
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !called)
        {
            Invoke("Fall", fallColdown);
            called = true;
        }
    }

}
