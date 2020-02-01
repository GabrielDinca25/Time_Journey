using UnityEngine;

public class TriggerBossFight : MonoBehaviour
{
    // The boss walls gameObject
    public GameObject bossWalls;

    // The boss game object
    public GameObject boss;

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this object
    /// </summary>
    /// <param name="other">The collider of the object that enters the trigger attached to this object</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bossWalls.SetActive(true);
            GameController.instance.SpecialAction = GetComponent<SpecialAction>().DoSpecialAction;
            boss.SetActive(true);
            //Disables the gameObject
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Disables the boss and boss walls
    /// </summary>
    public void Revert()
    {
        bossWalls.SetActive(false);
        boss.SetActive(false);
    }
}
