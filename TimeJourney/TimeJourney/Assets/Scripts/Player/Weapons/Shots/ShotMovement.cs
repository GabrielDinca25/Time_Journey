using UnityEngine;

public class ShotMovement : MonoBehaviour
{
    // The target of the shot
    public Vector3 target;

    // The speed of the shot
    public float speed = 1;

    // The destionation of the shot
    private Vector3 destination;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        destination = (target * 100 - transform.position * 100);
        Invoke("DisableShot", 0.7f);
    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    private void Update()
    {
        // Move the shot towards the destionation position
        transform.position = Vector2.MoveTowards(transform.position, destination * 100, Time.deltaTime * speed);
    }

    /// <summary>
    /// Disables shot
    /// </summary>
    private void DisableShot()
    {
        // Disables game object
        gameObject.SetActive(false);
    }
}
