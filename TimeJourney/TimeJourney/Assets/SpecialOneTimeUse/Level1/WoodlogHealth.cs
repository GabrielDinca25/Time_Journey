using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodlogHealth : Health
{
    // The gameObject of the watermap blocked by the woodlock
    public GameObject watermapBlocked;

    // The gameObject of the watermap freed after unblocking the log
    public GameObject watermapFree;

    // The gameObject of the lock that blocks the water
    public GameObject logCollider;

    /// <summary>
    /// Deals damage to woodlog
    /// </summary>
    /// <param name="dmgAmount">The amount of damage dealt</param>
    public override void GetDamage(int dmgAmount)
    {
        //Disables blocked water
        watermapBlocked.SetActive(false);

        //Enables free water
        watermapFree.SetActive(true);

        //Disables the log object and destroys it
        logCollider.SetActive(false);
        Destroy(gameObject);
    }
}