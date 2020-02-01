using UnityEngine;

public class GoblinBossAttack : MonoBehaviour
{
    // The parent object
    Transform parent;

    //The animator
    public Animator anim;

    // Boolean telling if the goblin should attack
    [SerializeField] private bool attack;

    // Boolean telling if the goblin is moving
    [SerializeField] private bool moving;

    // The next position that the goblin should move to
    private int nextPosition;

    // Array of coordinates to move to
    public Vector3[] positions;

    // The transform of the player
    [Tooltip("Player shot position because is his center")]
    public Transform player;

    // The player position
    private Vector3 playerPos;

    // Boolean telling if the goblin was idle
    private bool wasIdle;

    // Boolean telling if the goblin was idle once
    private bool onceIdle;

    // Time to reach the target
    public float timeToReachTarget;

    // Boolean telling if the goblin is facing right
    private bool faceRight;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    void Start()
    {
        parent = transform.parent;
        anim = GetComponent<Animator>();
        onceIdle = true;
        faceRight = true;
    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (!moving)
            {
                nextPosition = GetNextPosition();
                moving = true;
                wasIdle = true;
            }
            MovePosition(nextPosition);
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("AxeAttack"))
        {
            if (attack && wasIdle)
            {
                parent.position = Vector3.Lerp(parent.position, playerPos, timeToReachTarget);
            }
        }

    }

    /// <summary>
    /// Method checking and rotating the goblin left or right
    /// </summary>
    /// <param name="position">The position to rotate towards</param>
    private void CheckRotate(Vector3 position)
    {
        if (parent.position != position)
        {
            if (parent.position.x > position.x && faceRight)
            {
                parent.transform.Rotate(0f, 180f, 0f);
                faceRight = false;
            }
            else if (parent.position.x < position.x && !faceRight)
            {
                parent.transform.Rotate(0f, 180f, 0f);
                faceRight = true;
            }
        }
    }
    
    /// <summary>
    /// Get a random next position
    /// </summary>
    /// <returns></returns>
    public int GetNextPosition()
    {
        return Random.Range(0, positions.Length);
    }

    /// <summary>
    /// Moves goblin to given position
    /// </summary>
    /// <param name="nextPosition">The next position to move to</param>
    public void MovePosition(int nextPosition)
    {
        CheckRotate(positions[nextPosition]);
        parent.position = Vector2.MoveTowards(parent.position, positions[nextPosition], 1 * Time.deltaTime);

        if (parent.position == positions[nextPosition])
        {
            if (onceIdle)
            {
                Invoke("SetAttack", 1f);
                moving = false;
                onceIdle = false;
            }

        }
    }

    /// <summary>
    /// Sets the attack boolean to true
    /// </summary>
    public void Attack()
    {
        attack = true;
    }

    /// <summary>
    /// Sets the player position and flips the goblin according to it
    /// </summary>
    public void SetPlayerPos()
    {
        playerPos = player.position;
        CheckRotate(playerPos);
    }

    /// <summary>
    /// Sets the attack flag to false and invokes the idle animation
    /// </summary>
    public void StopAttack()
    {
        attack = false;
        wasIdle = false;
        Invoke("SetIdle", 1f);
    }

    /// <summary>
    /// Triggers the idle animation
    /// </summary>
    public void SetIdle()
    {
        anim.SetTrigger("Idle");
    }

    /// <summary>
    /// Triggers the attack animation
    /// </summary>
    public void SetAttack()
    {
        anim.SetTrigger("Attack");
        onceIdle = true;
    }
}
