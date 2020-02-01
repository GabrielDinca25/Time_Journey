using UnityEngine;

public class PickUpBomb : MonoBehaviour
{
    public Sprite deafult;
    public Sprite levitation;

    /// <summary>
    /// Method called every frame
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            enabled = false;
        }
        transform.position = Vector2.MoveTowards(transform.position, StoneAttacks.instance.cam.ScreenToWorldPoint(Input.mousePosition), 2f * Time.deltaTime);
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<SpriteRenderer>().sprite = levitation;
    }

    /// <summary>
    /// The function called when the behaviour becomes disabled.
    /// </summary>
    private void OnDisable()
    {
        GetComponent<Rigidbody2D>().gravityScale = 1;
        GetComponent<SpriteRenderer>().sprite = deafult;
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's collider
    /// </summary>
    /// <param name="other">The collider of the object that makes contact to the collider attached to this object</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        enabled = false;
    }

}
