using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "ChaseState", menuName = "States/ChaseState")]
public class ChaseState : StateSO
{
    public override void OnStateEnter(BomberController controller)
    {

    }

    public override void OnStateUpdate(BomberController controller)
    {
        controller.Agent.SetDestination(controller.Target.transform.position);
    }

    public override void OnStateExit(BomberController controller)
    {

    }
}
