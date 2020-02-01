using UnityEngine;

public class TargetActionHandler : MonoBehaviour
{
    public GameObject objectToMove;

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that enters the trigger attached to this object</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Shot"))
        {
            objectToMove.SetActive(true);
            Destroy(gameObject);
        }
    }
}
