using UnityEngine;

public class ShotFreeze : MonoBehaviour
{
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that enters the trigger attached to this object</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "BossPlayer")
        {
            other.GetComponent<FinalBossMovementV2>().Freeze();
            //Disables the gameObject
            gameObject.SetActive(false);
        }
    }
}
