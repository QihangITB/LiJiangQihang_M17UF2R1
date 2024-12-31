using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackState", menuName = "States/AttackState")]
public class AttackState : StateSO
{
    public override void OnStateEnter()
    {
        Debug.Log("AttackState");
    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
}
