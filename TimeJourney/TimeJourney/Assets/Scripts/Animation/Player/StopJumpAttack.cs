using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopJumpAttack : StateMachineBehaviour
{
    /// <summary>
    ///  Called on the first Update frame when a state machine evaluate this state.
    /// </summary>
    /// <param name="animator">The animator</param>
    /// <param name="stateInfo">The info of the state</param>
    /// <param name="layerIndex">The layer index of the sub-state machines</param>
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        StopJumpAttackReference sjar = animator.GetComponentInChildren<StopJumpAttackReference>();
        if(sjar != null)
        {
            sjar.pmws.canAttack = false;
        }
    }

    /// <summary>
    /// Called on the last update frame when a state machine evaluate this state.
    /// </summary>
    /// <param name="animator">The animator</param>
    /// <param name="stateInfo">The info of the state</param>
    /// <param name="layerIndex">The layer index of the sub-state machines</param>
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        StopJumpAttackReference sjar = animator.GetComponentInChildren<StopJumpAttackReference>();
        if (sjar != null)
        {
            sjar.pmws.canAttack = true;
        }
    }
}
