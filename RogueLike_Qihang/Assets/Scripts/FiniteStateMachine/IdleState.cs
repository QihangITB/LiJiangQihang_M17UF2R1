using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

[CreateAssetMenu(fileName = "IdleState", menuName = "States/IdleState")]
public class IdleState : StateSO
{
    private const string ParamIsMoving = "IsMoving";
    private const float WaitTime = 5f;

    private PatrolBehavior _patrol;
    private bool _canPatrol; // Indica si el agente está patrullando

    public override void OnStateEnter(BomberController controller)
    {
        _patrol = controller.GetComponent<PatrolBehavior>();
        _canPatrol = true;

        controller.Animator.SetBool(ParamIsMoving, true);
    }

    public override void OnStateUpdate(BomberController controller)
    {
        if (_patrol.HasArrivedToDestination() && _canPatrol)
        {
            _patrol.SetRandomDestination();
            controller.StartCoroutine(PatrolRestTime());
        }

        // Actualizar el parámetro IsMoving según el estado del agente
        bool isMoving = !_patrol.HasArrivedToDestination();
        controller.Animator.SetBool(ParamIsMoving, isMoving);
    }

    public override void OnStateExit(BomberController controller)
    {
        controller.StopCoroutine(PatrolRestTime());
        controller.Animator.SetBool(ParamIsMoving, false);
    }

    // Método para establecer un tiempo de descanso entre patrullajes
    private IEnumerator PatrolRestTime()
    {
        _canPatrol = false;
        yield return new WaitForSeconds(WaitTime);
        _canPatrol = true;
    }
}
