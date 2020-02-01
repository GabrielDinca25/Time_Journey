using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // The move speed of the paltform
    public float m_moveSpeed;

    // The start position of the platform
    private Vector3 startPosition;

    // The next position of the platform
    public Vector3 nextPosition;
    private Vector3 goPosition;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    void Start()
    {
        startPosition = transform.position;
        goPosition = nextPosition;
    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, goPosition, Time.deltaTime * m_moveSpeed);
        if (transform.position == goPosition)
        {
            if (transform.position == startPosition)
            {
                goPosition = nextPosition;
            }
            else
            {
                goPosition = startPosition;
            }
        }
    }
}
