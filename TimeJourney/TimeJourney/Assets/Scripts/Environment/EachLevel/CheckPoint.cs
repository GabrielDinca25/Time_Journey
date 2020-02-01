using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that enters the trigger attached to this object</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.instance.SaveGame();
            Destroy(gameObject);
        }
    }
}
