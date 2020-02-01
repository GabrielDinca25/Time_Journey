using UnityEngine;

public class EnemyShotStreightMovement : MonoBehaviour
{
    // The speed of the shot
    public float speed = 5;

    // The destination of the shot
    private Vector3 destination;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        destination = (transform.GetChild(1).position * 100 - transform.position * 100);
        Invoke("DisableShot", 5f);
    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination * 100, Time.deltaTime * speed);
    }


    private void DisableShot()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that enters the trigger attached to this object</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.instance.player.GetComponent<PlayerHealth>().GetDamage(30);
            Destroy(gameObject);
        }
    }
}
