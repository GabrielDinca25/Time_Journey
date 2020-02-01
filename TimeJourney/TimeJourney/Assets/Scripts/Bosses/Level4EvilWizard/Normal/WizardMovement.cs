using Pathfinding;
using UnityEngine;

public class WizardMovement : MonoBehaviour
{
    // The palyer shot position
    [Tooltip("Player ShotPosition(center of the player)")]
    public Transform playerShotPosition;

    // The speed of the wizard
    public float speed = 400f;

    // The next waypoint wizard
    public float nextWaypointDistance = 1f;

    // The scale of the wizard
    public float localScale = 1f;

    // The path of the wizard
    Path path;

    // The current waypoint
    int currentWaypoint = 0;

    // Boolean indicatin if the end of the path has been reached
    public bool reachedEndOfPath = false;

    // The seeker of the wizard
    Seeker seeker;

    // The rigid body of the wizard
    Rigidbody2D rb;

    // Boolean indicating if the wizard should attack
    public bool attack;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    public void OnEnable()
    {
        InvokeRepeating("UpdatePath", 0, .5f);
    }

    /// <summary>
    /// The function called when the behaviour becomes disabled.
    /// </summary>
    public void OnDisable()
    {
        CancelInvoke("UpdatePath");
    }

    void UpdatePath()
    {
        if (attack)
        {
            return;
        }
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, playerShotPosition.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    /// <summary>
    /// Method called every fixed frame-rate frame
    /// </summary>
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Move();
    }

    public void Move()
    {
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        //right
        if (transform.position.x <= playerShotPosition.position.x)
        {
            transform.localScale = new Vector3(-localScale, localScale, 0);
        }
        else if (transform.position.x >= playerShotPosition.position.x) //left
        {
            transform.localScale = new Vector3(localScale, localScale, 0);
        }
    }
}
