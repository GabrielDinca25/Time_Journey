using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    // The start position of the platform
    private Vector3 startPosition;

    // The fall cooldown of the platform
    public float fallColdown = 1f;

    // The fall time of the paltform
    public float fallTime = 1.5f;

    // The respawn time of the platform
    public float respawnTime = 2f;

    protected bool called;

    /// <summary>
    /// The method called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        startPosition = transform.position;
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's collider
    /// </summary>
    /// <param name="other">The collider of the object that makes contact to the collider attached to this object</param>
    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !called)
        {
            Invoke("Fall", fallColdown);
            called = true;
        }
    }

    void Fall()
    {
        gameObject.AddComponent<Rigidbody2D>();
        Invoke("Disable", fallTime);
    }

    void Disable()
    {
        Destroy(GetComponent<Rigidbody2D>());
        called = false;
        //Disables the gameObject
        gameObject.SetActive(false);
    }

    /// <summary>
    /// The function called when the behaviour becomes disabled.
    /// </summary>
    private void OnDisable()
    {
        Invoke("Respawn", respawnTime);
    }

    void Respawn()
    {
        gameObject.SetActive(true);
    }

}
