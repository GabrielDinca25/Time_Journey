using UnityEngine;

public class Level2GoblinSpecialAction : SpecialAction
{
    // Array of boxes gameObjects
    public GameObject[] boxes;

    // The multi switch manager instance
    public MultiSwitchManager msm;

    /// <summary>
    /// Does special action
    /// </summary>
    public override void DoSpecialAction()
    {
        gameObject.SetActive(true);
        GetComponent<TriggerBossFight>().Revert();
        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].SetActive(true);
        }
        msm.Reset();
    }
}
