using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogWithActionTrigger : MonoBehaviour
{
    // The dialog instance
    DialogWithAction dialog;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    private void Start()
    {
        dialog = GetComponent<DialogWithAction>();
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that enters the trigger attached to this object</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other is BoxCollider2D)
        {
            if (other.CompareTag("Player") && dialog != null)
            {
                dialog.enabled = true;
                dialog.dialogEnded = false;
            }
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that leaves the trigger attached to this object</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other is BoxCollider2D)
        {
            if (other.CompareTag("Player") && dialog != null)
            {
                dialog.CheckDialogStatus();
            }
        }
    }
}
