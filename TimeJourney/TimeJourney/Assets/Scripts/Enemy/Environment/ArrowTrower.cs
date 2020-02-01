using UnityEngine;

public class ArrowTrower : MonoBehaviour
{
    // The arrow game object
    public GameObject arrowPrefab;

    // The frequency of the arrow attacks
    public float frequency = 0.5f;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    private void Start()
    {
        InvokeRepeating("TrowArrows", 0, frequency);
    }

    /// <summary>
    /// Enables the arrow thrown
    /// </summary>
    public void TrowArrows()
    {
        GameObject nextArrow = GetNextArrow();
        nextArrow.transform.position = transform.position;
        nextArrow.SetActive(true);
    }

    /// <summary>
    /// Gets the next arrow to be thrown
    /// </summary>
    /// <returns></returns>
    public GameObject GetNextArrow()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (!gameObject.transform.GetChild(i).gameObject.activeSelf)
            {
                return gameObject.transform.GetChild(i).gameObject;
            }
        }
        GameObject newArrow = Instantiate(arrowPrefab, gameObject.transform.position, Quaternion.identity);
        newArrow.SetActive(false);
        newArrow.transform.SetParent(gameObject.transform);
        newArrow.GetComponent<ArrowMovement>().positionToReach = gameObject.transform.GetChild(0).GetComponent<ArrowMovement>().positionToReach;
        return newArrow;
    }
}