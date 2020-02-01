using UnityEngine;

public class BossEnter : MonoBehaviour
{
    // The player gameObject
    [HideInInspector] public GameObject player;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    void Start()
    {
        player = GameController.instance.player;
    }
}
