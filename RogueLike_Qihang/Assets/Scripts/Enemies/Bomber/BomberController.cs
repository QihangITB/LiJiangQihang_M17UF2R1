using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BomberController : MonoBehaviour
{
    [SerializeField] private Bomber _bomberData;
    public GameObject Target;
    public List<StateSO> States;

    private StateSO _currentState;
    private CircleCollider2D _visionCollider;
    private CapsuleCollider2D _bombCollider;

    private void Start()
    {
        _currentState = States[0];
        _currentState.OnStateEnter();

        _visionCollider = GetComponent<CircleCollider2D>();
        _visionCollider.radius = _bomberData.VisionRange;

        _bombCollider = transform.GetChild(0).GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Target.tag))
        {
            GoToState<ChaseState>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with target");

        if (collision.gameObject.CompareTag(Target.tag))
        {
            GoToState<AttackState>();
        }
    }

    private void GoToState<T>() where T : StateSO
    {
        if(_currentState.StatesToGo.Find(state => state is T))
        {
            _currentState.OnStateExit();
            _currentState = _currentState.StatesToGo.Find(obj => obj is T);
            _currentState.OnStateEnter();
        }
    }

}
