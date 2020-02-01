using UnityEngine;

public class TriggerBossFightWizard : MonoBehaviour
{
    // The boss walls gameObjects
    public GameObject bossWalls;
    
    // The portal gameObjects
    public GameObject portal;

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
            portal.SetActive(true);
            //Disables the gameObject
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Disables the boss walls
    /// </summary>
    public void Revert()
    {
        bossWalls.SetActive(false);
    }
}
