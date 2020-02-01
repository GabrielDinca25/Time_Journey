using UnityEngine;

public class SwithSwordAttack : StateMachineBehaviour
{
    /// <summary>
    /// Called at each Update frame except for the first and last frame.
    /// </summary>
    /// <param name="animator">The animator</param>
    /// <param name="stateInfo">The info of the state</param>
    /// <param name="layerIndex">The layer index of the sub-state machines</param>
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime <= 0.4f && animator.GetFloat("Speed") > 0.1f)
        {
            // Triggers sword attack animation if speed is greater than 0.1f
            animator.SetTrigger("SwitchSwordAttack");
            GameController.instance.swordLogic.GetComponent<SwordAttacks>().SwordDamage();
        }
    }
}
