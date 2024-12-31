using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChaseState", menuName = "States/ChaseState")]
public class ChaseState : StateSO
{
    public override void OnStateEnter()
    {
        Debug.Log("ChaseState");
    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
}
