using Pathfinding;
using UnityEngine;

public class EnemyAIMovement : MonoBehaviour
{
    // The position of the player attack
    [Tooltip("Player AttackPosition(center of the player)")]
    public Transform playerAttackPosition;

    // The array of possible patrolling positions
    public Vector3[] patrollingPositions;

    // The next position of the enemys
    private int nextPosition;

    // Bool indicating if the enemy is patrolling
    public bool patrolling;

    // The patrolling speed of the enemy
    public float speed = 400f;

    // The distance to the next waypoint
    public float nextWaypointDistance = 1f;

    // The path of the enemy
    Path path;
    
    // The current waypoint
    int currentWaypoint = 0;

    // Bool indicating if the end of the path has been reached
    public bool reachedEndOfPath = false;

    // The seeker of the enemy
    Seeker seeker;

    // The rigidbody of the enemy
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        nextPosition = 0;
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    public void OnEnable()
    {
        InvokeRepeating("UpdatePath", 0, .5f);
        patrolling = true;
    }

    /// <summary>
    /// The function called when the behaviour becomes disabled.
    /// </summary>
    public void OnDisable()
    {
        CancelInvoke("UpdatePath");
    }

    /// <summary>
    /// Updates the path of the wizarda
    /// </summary>
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            if (patrolling)
            {
                seeker.StartPath(rb.position, patrollingPositions[nextPosition], OnPathComplete);
            }
            else
            {
                seeker.StartPath(rb.position, playerAttackPosition.position, OnPathComplete);
            }
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
            nextPosition++;
            if (nextPosition >= patrollingPositions.Length)
            {
                nextPosition = 0;
            }
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
        Vector2 direction = ((Vector2)path.vectorPath[path.vectorPath.Count - 1] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        //right
        if (force.x >= 0.5f)
        {
            GetComponentInChildren<ParticleSystemRenderer>().flip = new Vector3(180f, 0, 0);
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (force.x <= -0.5f) //left
        {
            GetComponentInChildren<ParticleSystemRenderer>().flip = Vector3.zero;
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);

        }
    }
}
