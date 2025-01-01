using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BomberController : MonoBehaviour
{
    [SerializeField] private Bomber _bomberData;
    public GameObject Target;
    public List<StateSO> States;

    private StateSO _currentState;
    private CircleCollider2D _visionCollider;
    private NavMeshAgent _navMeshAgent;

    public NavMeshAgent Agent { get => _navMeshAgent; }

    private void Start()
    {
        _currentState = States[0];
        _currentState.OnStateEnter(this);

        _visionCollider = GetComponent<CircleCollider2D>();
        _visionCollider.radius = _bomberData.VisionRange;

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _bomberData.Speed;
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        _currentState.OnStateUpdate(this);
    }

    // Detecta si el objetivop esta dentro del rango de vision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Target.tag))
        {
            GoToState<ChaseState>();
        }
    }

    // Detecta la colision con la bomba
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with target");

        if (collision.gameObject.CompareTag(Target.tag))
        {
            //GoToState<AttackState>();
        }
    }

    private void GoToState<T>() where T : StateSO
    {
        if(_currentState.StatesToGo.Find(state => state is T))
        {
            _currentState.OnStateExit(this);
            _currentState = _currentState.StatesToGo.Find(obj => obj is T);
            _currentState.OnStateEnter(this);
        }
    }

}
