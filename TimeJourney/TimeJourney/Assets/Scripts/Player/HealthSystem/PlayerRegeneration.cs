using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRegeneration : MonoBehaviour
{
    PlayerHealth playerhealth;

    private bool m_damageReceive;
    public bool m_DamageReceive
    {
        get { return m_damageReceive; } 
        set { m_healthTimer = 0;
            m_damageReceive = value;
        }
    }
    public float m_healthTimer;
    public float m_RegenerationStartDelay;
    public float m_RegenerationAmoount;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    private void Start()
    {
        playerhealth = GetComponent<PlayerHealth>();
    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    void Update()
    {
        if(m_DamageReceive)
        {
            m_healthTimer += Time.deltaTime;
            if(m_healthTimer >= m_RegenerationStartDelay)
            {
                playerhealth.m_CurrentHealth += m_RegenerationAmoount;
                if(playerhealth.m_CurrentHealth > 100)
                {
                    playerhealth.m_CurrentHealth = 100;
                    m_DamageReceive = false;
                }
            }
        }
    }

}
