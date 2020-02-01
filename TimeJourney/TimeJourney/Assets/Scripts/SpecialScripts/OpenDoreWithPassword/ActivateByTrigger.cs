using UnityEngine;

public class ActivateByTrigger : MonoBehaviour
{
    // The sword logic gameObject
    public GameObject m_SwordLogic;

    // The canvas gameObject
    public GameObject m_Canvas;

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that enters the trigger attached to this object</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            m_SwordLogic.SetActive(false);
            m_Canvas.SetActive(false);
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that leaves the trigger attached to this object</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Disable();
            GetComponent<TypePassword>().ResetText();
        }
    }

    /// <summary>
    /// Disables objects
    /// </summary>
    public void Disable()
    {
        //Disables the gameObject
        transform.GetChild(0).gameObject.SetActive(false);
        m_SwordLogic.SetActive(true);
        m_Canvas.SetActive(true);
    }
}
