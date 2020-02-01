using UnityEngine;

public class PlayerMovementWithSword : MonoBehaviour
{
    // The character controller
    public CharacterController2D controller;

    // The animator
    [HideInInspector] public Animator animator;

    // The running speed of the player
    public float runSpeed;

    // The horizontal movement of the player
    [HideInInspector] public float horizontalMove;

    // Bool indicating wether the player should jump
    private bool jump = false;

    // Bool indicating if the player can attack
    public bool canAttack;
    
    // The stone attacks object
    [Tooltip("The script for StoneAttack from stone logic")]
    public StoneAttacks stoneAttacks;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    private void Start()
    {
        // Gets the animator component from the object
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        canAttack = true;
    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    void Update()
    {
        // Get horizontal moving from the horizontal axis and multiply with runSpeed 
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        // Check if user press the jump button and trigger the jumping
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            Jump();
        }
    }

    /// <summary>
    /// Method called every fixed frame-rate frame
    /// </summary>
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void Jump()
    {
        animator.SetBool("IsJumping", true);
    }

    public void DisableAttack()
    {
        canAttack = false;
    }

    public void EnableAttack()
    {
        canAttack = true;
        GameController.instance.swordLogic.GetComponent<SwordAttacks>().StopAttacking();
    }

    public void Shot()
    {
        stoneAttacks.Shot();
    }
}
