using UnityEngine;

public class PopUpManager : MonoBehaviour {

    /// <summary>
    /// can be extended to disable when button string ButtonName was pressed
    /// </summary>
    [HideInInspector]public GameObject Child;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    private void Start()
    {
        Child = transform.GetChild(0).gameObject;
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that enters the trigger attached to this object</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Child.SetActive(true);
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that leaves the trigger attached to this object</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Child.SetActive(false);
        }
    }

}
