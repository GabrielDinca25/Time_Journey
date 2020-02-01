public class EnemyRollingHealth : Health
{
    // Boolean indicating if the enemy should get damage
    public bool getDMG;

    /// <summary>
    /// Deals damage to goblin
    /// </summary>
    /// <param name="dmgAmount">The damage amount</param>
    public override void GetDamage(int dmgAmount)
    {
        if (getDMG)
        {
            base.GetDamage(dmgAmount);
        }
    }

    /// <summary>
    /// Deals damage to goblin according to the attack type
    /// </summary>
    /// <param name="type">The type of the attack</param>
    /// <param name="dmgAmount">The damage amount</param>
    public override void GetDamage(string type, int dmgAmount)
    {
        if (getDMG)
        {
            base.GetDamage(type, dmgAmount);
        }
    }
}
