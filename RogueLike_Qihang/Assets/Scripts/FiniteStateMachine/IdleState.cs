using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "IdleState", menuName = "States/IdleState")]
public class IdleState : StateSO
{
    private const float WaitTime = 5f;

    private PatrolBehavior _patrol;
    private bool _isPatrolling;

    public override void OnStateEnter(BomberController controller)
    {
        _patrol = controller.GetComponent<PatrolBehavior>();
        _isPatrolling = true;
    }

    public override void OnStateUpdate(BomberController controller)
    {
        if(_patrol.HasArrivedToDestination() && _isPatrolling)
        {
            _patrol.SetRandomDestination();
            controller.StartCoroutine(PatrolRestTime());
        }
    }

    public override void OnStateExit(BomberController controller)
    {
        controller.StopCoroutine(PatrolRestTime());
    }

    private IEnumerator PatrolRestTime()
    {
        _isPatrolling = false;
        yield return new WaitForSeconds(WaitTime);
        _isPatrolling = true;
    }
}
