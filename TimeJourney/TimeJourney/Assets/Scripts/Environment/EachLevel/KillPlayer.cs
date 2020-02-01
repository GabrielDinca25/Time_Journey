using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that enters the trigger attached to this object</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // If the object is a player the game is over
            GameController.instance.GameOver();
        }
        if (other.tag == "Enemy")
        {
            // If the object is an enemy it is disabled
            other.gameObject.SetActive(false);
        }
    }
}
