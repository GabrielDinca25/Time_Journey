using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnTrigger : MonoBehaviour
{
    // The moving speed of the object
    public float moveSpeed;

    // The next position to move tos
    public Vector3 nextPosition;

    /// <summary>
    /// Method called every frame
    /// </summary>
    void Update()
    {
        // The object is moved towards the next position
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, Time.deltaTime * moveSpeed);
        if (gameObject.transform.position == nextPosition)
        {
            //Destroy the script on the object if the object is already at that position
            Destroy(GetComponent<MoveOnTrigger>());
        }
    }
}
