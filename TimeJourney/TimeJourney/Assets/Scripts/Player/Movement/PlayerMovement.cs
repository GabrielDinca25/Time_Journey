using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // The animator
    public Animator animator;

    // The rigid body of the player
    public Rigidbody2D m_Rigidbody2D;

    // The running speed of the player
    public float runSpeed = 30f;

    // For determining which way the player is currently facing.
    public bool m_FacingRight;
    
    // Wether the player can move left
    public bool canMoveLeft = false;

    // The horizontal moving
    float horizontalMove = 0f;

    // How much to smooth out the movement
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;

    // The velocity of the player
    private Vector3 m_Velocity = Vector3.zero;

    /// <summary>
    /// The method called once per frame
    /// </summary>
    void Update()
    {
        // Get horizontal moving from the horizontal axis and multiply with runSpeed 
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (horizontalMove < 0 && !canMoveLeft)
        {
           horizontalMove = 0;    
        }

        // Set the speed value of the animator to trigger the running animation
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }

    /// <summary>
    /// Method called every fixed frame-rate frame
    /// </summary>
    void FixedUpdate()
    {
        // Moves the player towards the given direction
        Move(horizontalMove * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Moves the player with specified value
    /// </summary>
    /// <param name="move">The value of the movement</param>
    public void Move(float move)
    {
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

    /// <summary>
    /// Flips the player towards specified destination
    /// </summary>
    /// <param name="moveDistantion">The specified destination</param>
    private void Flip(float moveDistantion)
    {
        // Invert the facing right flag
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180f, 0f);
        transform.position += new Vector3(moveDistantion, 0, 0);
    }

}
