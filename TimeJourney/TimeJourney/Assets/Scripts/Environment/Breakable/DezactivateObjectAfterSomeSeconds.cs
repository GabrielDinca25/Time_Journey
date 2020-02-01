using UnityEngine;

public class DezactivateObjectAfterSomeSeconds : MonoBehaviour
{
    // Delay time
    public float delayTime = 2f;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        Invoke("Disable", delayTime);
    }

    /// <summary>
    /// Disables the gameObjects
    /// </summary>
    private void Disable()
    {
        // Destroyes gameObject
        Destroy(gameObject);
    }

}
