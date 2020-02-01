using Pathfinding;
using UnityEngine;

public class PurpleGoblinMovementAI : MonoBehaviour
{

    // The position of the player shot
    [Tooltip("Player ShotPosition(center of the player)")]
    public Transform playerShotPosition;

    // The speed of the goblin
    public float speed = 400f;

    // The distance to the next Waypoint
    public float nextWaypointDistance = 1f;

    // The path of the goblin
    Path path;

    // The current waypoint
    int currentWaypoint = 0;

    // Boolean indicating if the goblin has reached the end of the path
    public bool reachedEndOfPath = false;

    // The seeker of the goblin
    Seeker seeker;

    // The rigidbody of the goblin
    Rigidbody2D rb;

    // The enemy GFX
    private Transform enemyGFX;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        enemyGFX = transform.GetChild(0);
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

    /// <summary>
    /// Updates the path of the goblin
    /// </summary>
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, playerShotPosition.position, OnPathComplete);
        }
    }

    /// <summary>
    /// Called when path is completed
    /// </summary>
    /// <param name="p"></param>
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

    /// <summary>
    /// Moves the goblin according to the path
    /// </summary>
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
            enemyGFX.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (transform.position.x >= playerShotPosition.position.x) //left
        {
            enemyGFX.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
