using UnityEngine;

public class Health : MonoBehaviour
{
    // The max default health points
    public int m_maxHp = 100;

    // The current health of the object
    public int m_CurrentHealth;

    public virtual void Start()
    {
        m_CurrentHealth = m_maxHp;
    }

    public virtual void GetDamage(int dmgAmount)
    {
    }

    public virtual void GetDamage(string type, int dmgAmount)
    {
    }

    public virtual void Die()
    {
        gameObject.SetActive(false);
    }


    /// <summary>
    /// Gets and starts damage animation
    /// </summary>
    public virtual void GetDamageAnimation()
    {
    }

}
