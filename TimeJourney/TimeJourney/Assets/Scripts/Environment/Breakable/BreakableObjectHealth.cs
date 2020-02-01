using UnityEngine;

public class BreakableObjectHealth : Health
{
    // The object to replace the broken one
    public GameObject objectToSwitch;

    /// <summary>
    /// Destroys the breakable object
    /// </summary>
    public override void Die()
    {
        Instantiate(objectToSwitch, transform.position, Quaternion.identity);
        //Disables the gameObject
        gameObject.SetActive(false);
    }
}
