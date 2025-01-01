using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleState", menuName = "States/IdleState")]
public class IdleState : StateSO
{
    public override void OnStateEnter(BomberController controller)
    {
        Debug.Log("IdleState");
    }

    public override void OnStateUpdate(BomberController controller)
    {

    }

    public override void OnStateExit(BomberController controller)
    {

    }
}
