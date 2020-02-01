﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image m_HurtImage;
    public float m_maxHp;

    [HideInInspector] public Animator m_animator;
    private IEnumerator damageAnimation;

    public SpriteRenderer[] bodyParts;

    private bool damageReceived;


    [SerializeField] private float m_currentHealth;
    public float m_CurrentHealth
    {
        get { return m_currentHealth; }
        set
        {
            m_currentHealth = value;
            SetDamageImage();
        }
    }

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_CurrentHealth = m_maxHp;
    }

    public void GetDamage(int dmgAmount, bool specialAction = false)
    {
        if (damageReceived)
        {
            return;
        }
        damageReceived = true;
        m_CurrentHealth -= dmgAmount;
        if (m_CurrentHealth <= 0)
        {
            if (specialAction)
            {
                GameController.instance.DoSpecialAction();
            }
            Die();
            return;
        }
        GetDamageAnimation();
    }

    /// <summary>
    /// Gets and starts damage animation
    /// </summary>
    public void GetDamageAnimation()
    {
        damageAnimation = DamageAnimation();
        StartCoroutine(damageAnimation);
    }

    /// <summary>
    /// `The Damage animation, makes the Goblin blink
    /// </summary>
    /// <returns>IEnumarator for the interval of the blinks</returns>
    public IEnumerator DamageAnimation()
    {
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < bodyParts.Length; j++)
            {
                bodyParts[j].enabled = !bodyParts[j].enabled;
            }
            yield return new WaitForSeconds(.1f);
        }
        GetComponent<PlayerRegeneration>().m_DamageReceive = true;
        damageReceived = false;
    }


    public void Die()
    {
        TriggerDeathAnimation();
        Death();
    }

    public void TriggerDeathAnimation()
    {
        m_animator.SetFloat("Speed", 0);
        m_animator.SetBool("IsGrounded", true);
    }

    public void Death() // called after death animation;
    {
        GameController.instance.GameOver();
    }

    public void SetDamageImage()
    {
        float currentHPLost = (m_maxHp - m_currentHealth) / 100;
        m_HurtImage.color = new Color(1, 0, 0, currentHPLost);
    }

    public void Revive()
    {
        m_currentHealth = m_maxHp;
        SetDamageImage();
        damageReceived = false;
    }

}