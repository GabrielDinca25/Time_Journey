public class Level4PlayerBossSpecialAction : SpecialAction
{
    /// <summary>
    /// Does special action
    /// </summary>
    public override void DoSpecialAction()
    {
        gameObject.SetActive(true);
        GetComponent<TriggerBossFight>().Revert();
    }
}
