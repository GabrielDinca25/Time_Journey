using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    // The speed of the arrow
    public float speed = 200;

    // The position for the arrow to reach
    public Vector3 positionToReach;

    // The rigid body of the arrow
    private Rigidbody2D rb2d;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        var dir = positionToReach - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rb2d.AddForce(transform.right * speed);
        rb2d.angularVelocity = 200f;
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        if (rb2d != null)
        {
            rb2d.AddForce(transform.right * speed);
            rb2d.angularVelocity = 200f;
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that enters the trigger attached to this object</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameController.instance.GameOver();
        }
        if (other.gameObject.tag == "Shot")
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "BackGround" || other.gameObject.tag == "Breakable" || other.gameObject.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }
    }
}
