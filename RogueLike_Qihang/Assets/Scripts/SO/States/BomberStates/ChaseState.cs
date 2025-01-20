using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "ChaseState", menuName = "States/Bomber/ChaseState")]
public class ChaseState : StateSO
{
    private const string ParamHasTarget = "HasTarget";
    public override void OnStateEnter(BomberController controller)
    {
        controller.Animator.SetBool(ParamHasTarget, true);
    }

    public override void OnStateUpdate(BomberController controller)
    {
        controller.Agent.SetDestination(controller.Target.transform.position);
    }

    public override void OnStateExit(BomberController controller)
    {
        controller.Animator.SetBool(ParamHasTarget, false);
        controller.Agent.ResetPath();
    }
}
