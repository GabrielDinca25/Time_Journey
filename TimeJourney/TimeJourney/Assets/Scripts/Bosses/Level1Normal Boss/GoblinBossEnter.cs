using UnityEngine;

public class GoblinBossEnter : BossEnter
{
    /// <summary>
    /// Method called every frame
    /// </summary>
    private void Update()
    {
        // Increases the size of the object with 0.001f every frame
        transform.localScale += new Vector3(0.001f, 0.001f, 0);

        if (transform.localScale.x >= 0.1f)
        {
            // Sets gameObject size to 0.1f if the size is greater
            transform.localScale = new Vector3(0.1f, 0.1f, 0);

            // Enables the attack of the goblin
            GetComponentInChildren<GoblinBossAttack>().enabled = true;
            enabled = false;
        }
    }
}
