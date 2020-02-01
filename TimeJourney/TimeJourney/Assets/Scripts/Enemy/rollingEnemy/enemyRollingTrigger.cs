using UnityEngine;

public class enemyRollingTrigger : MonoBehaviour
{
    // The enemy rolling movement component
    private EnemyRollingMovement erm;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    private void Start()
    {
        erm = GetComponentInParent<EnemyRollingMovement>();
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that enters the trigger attached to this object</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            //set rotation;
            erm.SetRotation(true);

            // select direction
            if (gameObject.transform.position.x > other.transform.position.x)
            {
                erm.Roll = erm.RollLeft;
            }
            else
            {
                erm.Roll = erm.RollRight;
            }
            erm.roll = true;
        }
    }
}
