using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "AttackState", menuName = "States/Bomber/AttackState")]
public class AttackState : StateSO
{
    private const string ParamAttack = "Attack";

    public override void OnStateEnter(BomberController controller)
    {
        controller.Animator.SetTrigger(ParamAttack);

        controller.Agent.isStopped = true;
        controller.Agent.ResetPath(); // Detener por completo el movimiento

        controller.Agent.speed = 0;
    }

    public override void OnStateUpdate(BomberController controller)
    {

    }

    public override void OnStateExit(BomberController controller)
    {

    }
}
