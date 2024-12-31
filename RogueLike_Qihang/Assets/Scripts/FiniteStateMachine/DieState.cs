using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DieState", menuName = "States/DieState")]
public class DieState : StateSO
{
    private const string ParamDie = "Die";

    public override void OnStateEnter()
    {
        Debug.Log("DieState");
    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
}
