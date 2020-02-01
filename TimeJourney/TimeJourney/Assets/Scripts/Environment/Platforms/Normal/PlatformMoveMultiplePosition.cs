using UnityEngine;

public class PlatformMoveMultiplePosition : MonoBehaviour
{
    // The move speed of the platform
    public float m_moveSpeed = 1;

    // The array of positions where the platforms should move
    public Vector3[] m_Positions;

    // The next position of the platform
    private Vector3 goPosition;

    // The index of the current position of the platforms
    private int currentPosition;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    void Start()
    {
        currentPosition = 0;
        goPosition = m_Positions[currentPosition];
    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, goPosition, Time.deltaTime * m_moveSpeed);
        if (transform.position == goPosition)
        {
            currentPosition++;
            if (currentPosition >= m_Positions.Length)
            {
                currentPosition = 0;
            }
            goPosition = m_Positions[currentPosition];
        }
    }
}
