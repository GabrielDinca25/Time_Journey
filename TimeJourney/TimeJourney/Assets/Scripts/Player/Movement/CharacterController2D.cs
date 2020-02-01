using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    // Amount of force added when the player jumps.
    public float m_JumpForce = 200f;

    // How much to smooth out the movement
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;

    // Whether or not a player can steer while jumping;
    [SerializeField] private bool m_AirControl = false;

    // A mask determining what is ground to the character
    public LayerMask m_WhatIsGround;

    // A position marking where to check if the player is grounded.
    public Transform m_GroundCheck;

    // Radius of the overlap circle to determine if grounded
    const float k_GroundedRadius = .05f;

    // Whether or not the player is grounded.
    public bool m_Grounded;    

    // The rigid body of the player
    private Rigidbody2D m_Rigidbody2D;

    // For determining which way the player is currently facing.
    public bool m_FacingRight;

    // The velocity of the player
    private Vector3 m_Velocity = Vector3.zero;

    // The animator
    public Animator animator;

    [Header("Events")]
    [Space]
    // The OnLand event
    public UnityEvent OnLandEvent;

    /// <summary>
    /// The method called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
    }

    /// <summary>
    /// Method called every fixed frame-rate frame
    /// </summary>
    private void FixedUpdate()
    {
        m_Grounded = false;
        animator.SetBool("IsGrounded", false);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                animator.SetBool("IsGrounded", true);
                animator.SetBool("IsJumping", false);
                OnLandEvent.Invoke();
                return;
            }
        }
        animator.SetBool("IsJumping", true);
    }

    /// <summary>
    /// Moves the player
    /// </summary>
    /// <param name="move">The move value</param>
    /// <param name="jump">Where the player is jumping or not</param>
    public void Move(float move, bool jump)
    {
        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // sets the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);

            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            if (move > 0 && !m_FacingRight)
            {
                // if player is moving towards right and it not facing right, then flip it
                Flip(0.3f);
            }
            else if (move < 0 && m_FacingRight)
            {
                // if player is moving towards left and it not facing left, then flip it
                Flip(-0.3f);
            }
        }

        if (m_Grounded && jump)
        {
            // if player is grounded and should jump, apply vertical force
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            SetGrounded();
        }
    }

    /// <summary>
    /// Sets the grounded flag
    /// </summary>
    public void SetGrounded()
    {
        m_Grounded = false;
        animator.SetBool("IsGrounded", false);
    }

    /// <summary>
    /// Flips the player towards the move destination
    /// </summary>
    /// <param name="moveDistantion">The move destionation</param>
    private void Flip(float moveDistantion)
    {
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180f, 0f);
        // change orientation
        transform.position += new Vector3(moveDistantion, 0, 0);
    }
}
