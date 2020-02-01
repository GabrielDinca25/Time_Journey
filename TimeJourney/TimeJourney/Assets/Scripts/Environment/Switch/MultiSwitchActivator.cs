using UnityEngine;

public class MultiSwitchActivator : MonoBehaviour
{
    public bool state;
    private SpriteRenderer sr;

    public Sprite state1;
    public Sprite state2;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that enters the trigger attached to this object</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shot"))
        {
            SwitchState();
            GetComponentInParent<MultiSwitchManager>().UpdateStateOfSwitch(name);
            //Disables the gameObject
            other.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Switches sprite state
    /// </summary>
    private void SwitchState()
    {
        state = !state;

        if (sr.sprite == state1)
        {
            sr.sprite = state2;
        }
        else
        {
            sr.sprite = state1;
        }
    }
}
