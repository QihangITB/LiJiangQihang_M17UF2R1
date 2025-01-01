using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DieState", menuName = "States/DieState")]
public class DieState : StateSO
{
    public override void OnStateEnter(BomberController controller)
    {
        Destroy(controller.gameObject);
    }

    public override void OnStateUpdate(BomberController controller)
    {

    }

    public override void OnStateExit(BomberController controller)
    {

    }
}
