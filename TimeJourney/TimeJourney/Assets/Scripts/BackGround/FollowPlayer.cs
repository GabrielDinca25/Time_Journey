using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // the target to follow
    public Transform target;

    /// <summary>
    /// Method called after all Update functions have been called.    
    /// </summary>
    void LateUpdate()
    {
        // Moves the background at the target position
        transform.position = new Vector3(target.position.x, 10f, 0);
    }
}
