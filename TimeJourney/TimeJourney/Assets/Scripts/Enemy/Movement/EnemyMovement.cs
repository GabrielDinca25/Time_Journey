using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Bool indicating if the player is in sight
    public bool m_playerInSight;
    // The movement speed of the enemy
    public float m_movementSpeed;
    // The animator
    protected Animator anim;
    // The rigid body of the enemy
    protected Rigidbody2D rb2d;

    /// <summary>
    /// Patrol
    /// </summary>
    protected virtual void Patrol() {; }

    /// <summary>
    /// Chase player
    /// </summary>
    protected virtual void ChasePlayer() {; }

    /// <summary>
    /// Attack
    /// </summary>
    protected virtual void Attack() {; }

    /// <summary>
    /// Flip
    /// </summary>
    protected virtual void Flip() {; }

    /// <summary>
    /// PlayerInSight
    /// </summary>
    public virtual void PlayerInSight() {; }

    /// <summary>
    /// PlayerOutOfSight
    /// </summary>
    public virtual void PlayerOutOfSight() {; }

}
