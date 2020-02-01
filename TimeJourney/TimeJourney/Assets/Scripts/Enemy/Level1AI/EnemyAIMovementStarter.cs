using UnityEngine;

public class EnemyAIMovementStarter : MonoBehaviour
{
    /// <summary>
    /// Called when the renderer became visible by any camera.
    /// </summary>
    private void OnBecameVisible()
    {
        //check if we already send a stop moving request
        //if so stop it 
        CancelInvoke("DisableAi");

        GetComponentInParent<EnemyAIMovement>().enabled = true;
    }

    /// <summary>
    /// Called when the renderer became invisible by any camera.
    /// </summary>
    private void OnBecameInvisible()
    {
        Invoke("DisableAi", 10f);
    }

    /// <summary>
    /// Disable the enemy AI movement
    /// </summary>
    void DisableAi()
    {
        GetComponentInParent<EnemyAIMovement>().enabled = false;
    }
}
