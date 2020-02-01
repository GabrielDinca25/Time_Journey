using System;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttacks : MonoBehaviour
{
    // The FireWeapon function delegate
    public Action FireWeapon = delegate { };

    // The player movement component
    private PlayerMovementWithSword pmws;

    // The position of the attack
    public Transform attackPos;

    // Layer mask for what is considered an enemy
    public LayerMask whatIsEnemies;

    // Layer mask for what is considered an enemy trigger
    public LayerMask whatIsEnemiesTrigger;

    // The range of the sword attack
    public float attackRange = 0.8f;

    // The sword damage amount
    public int swordDamageAmount;

    // List of enemies which received damage
    private List<GameObject> enemyWhoReceivedDamage = new List<GameObject>();

    void Start()
    {
        // Get the player movement component
        pmws = GameController.instance.player.GetComponent<PlayerMovementWithSword>();
        FireWeapon = SwordAttack;
    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    void Update()
    {
        // Check input for mouse button for attacking
        if (Input.GetMouseButtonDown(0) && pmws.canAttack)
        {
            SwordAttack();
        }
    }

    /// <summary>
    /// Starts the sword attack
    /// </summary>
    private void SwordAttack()
    {
        SwordAnimation();
        // Repeats sword damage method
        InvokeRepeating("SwordDamage", 0, .1f);
    }

    /// <summary>
    /// Starts the sword animation
    /// </summary>
    public void SwordAnimation()
    {
        pmws.canAttack = false;
        pmws.animator.SetTrigger("SwordAttack");
    }

    /// <summary>
    /// Deals the sword damage
    /// </summary>
    public void SwordDamage()
    {
        // Overlap range circle with objects the sword cand damage
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

        for (int i = 0; i < enemiesToDamage.Length; ++i)
        {
            if (!enemiesToDamage[i].isTrigger)
            {
                // Deal damage to all objects that are hit by the sword
                if (!enemyWhoReceivedDamage.Contains(enemiesToDamage[i].gameObject))
                {
                    enemiesToDamage[i].GetComponent<Health>().GetDamage(swordDamageAmount);
                    enemyWhoReceivedDamage.Add(enemiesToDamage[i].gameObject);
                }
            }
        }

        enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemiesTrigger);

        for (int i = 0; i < enemiesToDamage.Length; ++i)
        {
            if (enemiesToDamage[i].isTrigger)
            {
                if (!enemyWhoReceivedDamage.Contains(enemiesToDamage[i].gameObject))
                {
                    enemiesToDamage[i].GetComponent<Health>().GetDamage(swordDamageAmount);
                    enemyWhoReceivedDamage.Add(enemiesToDamage[i].gameObject);
                }
            }
        }
    }

    /// <summary>
    /// Stopes the player attacking
    /// </summary>
    public void StopAttacking()
    {
        CancelInvoke("SwordDamage");
        enemyWhoReceivedDamage.Clear();
    }
}
