using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DieState", menuName = "States/Bomber/DieState")]
public class DieState : StateSO
{
    public override void OnStateEnter(BomberController controller)
    {
        controller.SaveIdleState();
        // Guardamos el enemigo en el pool en lugar de destruirlos
        BomberSpawner.PushBomber(controller.gameObject);
    }

    public override void OnStateUpdate(BomberController controller)
    {

    }

    public override void OnStateExit(BomberController controller)
    {

    }
}
