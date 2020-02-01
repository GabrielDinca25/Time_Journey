using System;
using UnityEngine;

public class EnemyRollingMovement : MonoBehaviour
{
    // The enemy damage amount
    public int m_enemyDamageAmount;

    // The speed of the enemy
    public float speed;

    // The enemy start position
    private Vector3 startPosition;

    // Bool indicating if enemy is rolling
    public bool roll;

    // Roll function delegate
    public Action Roll = delegate { };

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    private void Start()
    {
        startPosition = gameObject.transform.position;
    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    private void Update()
    {
        if (roll)
        {
            Roll();
        }
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void RollRight()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.right, Time.deltaTime * speed);
    }

    public void RollLeft()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position - transform.right, Time.deltaTime * speed);
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's collider
    /// </summary>
    /// <param name="other">The collider of the object that makes contact to the collider attached to this object</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            GameController.instance.player.GetComponent<PlayerHealth>().GetDamage(m_enemyDamageAmount);
            //Disables the gameObject
            gameObject.SetActive(false);
        }
        if (other.gameObject.tag.Equals("Breakable"))
        {
            other.gameObject.GetComponent<Health>().Die();
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// The function called when the behaviour becomes disabled.
    /// </summary>
    private void OnDisable()
    {
        roll = false;
        gameObject.transform.position = startPosition;
        SetRotation(false);
        Invoke("Enable", 2f);
    }

    private void Enable()
    {
        gameObject.SetActive(true);
    }

    public void SetRotation(bool status)
    {
        var rotation = GetComponentInChildren<ParticleSystem>().rotationOverLifetime;
        if (status)
        {
            rotation.zMultiplier = 1500;
        }
        else
        {
            rotation.zMultiplier = 0;
        }
    }
}
