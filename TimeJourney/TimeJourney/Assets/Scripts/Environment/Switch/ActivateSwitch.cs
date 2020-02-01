using UnityEngine;

public class ActivateSwitch : MonoBehaviour
{
    public GameObject platformToMove;
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
        platformToMove.GetComponent<MovePlatform>().m_GoToNextPosition = state;
        platformToMove.GetComponent<MovePlatform>().enabled = false;
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
            platformToMove.GetComponent<MovePlatform>().m_GoToNextPosition = state;
            platformToMove.GetComponent<MovePlatform>().enabled = true;
            //Disables the gameObject
            other.gameObject.SetActive(false);
        }
    }

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
