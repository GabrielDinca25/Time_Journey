using UnityEngine;

public class PlatformMoveOneTimeWithoutDestroying : MonoBehaviour
{
    // The next position of the platforms
    public Vector3 nextPosition;

    /// <summary>
    /// Method called every frame
    /// </summary>
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, Time.deltaTime * 2);
        if (transform.position == nextPosition)
        {
            Destroy(this);
        }
    }
}
