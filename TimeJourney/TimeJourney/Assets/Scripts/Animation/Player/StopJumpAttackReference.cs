using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopJumpAttackReference : MonoBehaviour
{
    // The player object
    public PlayerMovementWithSword pmws;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    void Start()
    {
        pmws = GetComponentInParent<PlayerMovementWithSword>();
    }
}
