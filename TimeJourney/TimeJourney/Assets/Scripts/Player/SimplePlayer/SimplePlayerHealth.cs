﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimplePlayerHealth : MonoBehaviour
{
    public Image m_HurtImage;
    public float m_maxHp;

    private bool damageReceived;

    [SerializeField] private float m_currentHealth;
    public float m_CurrentHealth
    {
        get { return m_currentHealth; }
        set
        {
            m_currentHealth = value;
            SetHealth();
        }
    }

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    private void Start()
    {
        m_CurrentHealth = m_maxHp;
    }

    public void GetDamage(int dmgAmount)
    {
        m_CurrentHealth -= dmgAmount;
        if (m_CurrentHealth <= 0)
        {
            Die();
            return;
        }   
    }

    /// <summary>
    /// Kills the player
    /// </summary>
    public void Die()
    {
        transform.position = Vector3.zero;
        m_currentHealth = 100;
        SetHealth();
    }

    /// <summary>
    /// Sets health of the player
    /// </summary>
    public void SetHealth()
    {
        float currentHPLost = (m_maxHp - m_currentHealth) / 100;
        m_HurtImage.color = new Color(1, 0, 0, currentHPLost);
    }

}
