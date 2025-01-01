using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "AttackState", menuName = "States/AttackState")]
public class AttackState : StateSO
{
    private const string ParamAttack = "Attack";
    private const float ExplosionRange = 2f;

    public override void OnStateEnter(BomberController controller)
    {
        controller.Agent.isStopped = true;
        controller.Animator.SetTrigger(ParamAttack);
        controller.BombArea.size = new Vector2(ExplosionRange, ExplosionRange);
    }

    public override void OnStateUpdate(BomberController controller)
    {

    }

    public override void OnStateExit(BomberController controller)
    {

    }
}
