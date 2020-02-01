using UnityEngine;

public class ShotCollision : MonoBehaviour
{
    // The type of the shot
    public string Type;

    // The type damage amout
    public int ShotDamageAmount;

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that enters the trigger attached to this object</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Contains("Ground") || other.tag.Equals("Breakable"))
        {
            // Disables the gameObject
            gameObject.SetActive(false);
            return;
        }

        if (other.tag.Equals("Enemy") && !other.gameObject.name.Contains("Shot"))
        {
            other.GetComponent<Health>().GetDamage(Type, ShotDamageAmount);
            // Disables the gameObject
            gameObject.SetActive(false);
        }
    }
}
