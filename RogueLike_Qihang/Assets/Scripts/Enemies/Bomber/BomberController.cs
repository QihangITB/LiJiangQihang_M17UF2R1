using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BomberController : MonoBehaviour
{
    [SerializeField] private Bomber _bomberData;
    public Animator Animator;
    public GameObject Target;
    public List<StateSO> States;

    private StateSO _currentState;
    private CircleCollider2D _visionCollider;
    private CapsuleCollider2D _bombCollider;
    private NavMeshAgent _navMeshAgent;

    public CapsuleCollider2D BombArea { get => _bombCollider; set => _bombCollider = value; }
    public NavMeshAgent Agent { get => _navMeshAgent; }

    private void Start()
    {
        _visionCollider = GetComponent<CircleCollider2D>();
        _visionCollider.radius = _bomberData.VisionRange;

        _bombCollider = GetComponent<CapsuleCollider2D>();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _bomberData.Speed;
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;

        _currentState = States[0];
        _currentState.OnStateEnter(this);
    }

    private void Update()
    {
        _currentState.OnStateUpdate(this);
    }

    private void GoToState<T>() where T : StateSO
    {
        Debug.Log("Old state " + _currentState.name);
        if (_currentState.StatesToGo.Find(state => state is T))
        {
            _currentState.OnStateExit(this);
            _currentState = _currentState.StatesToGo.Find(obj => obj is T);
            _currentState.OnStateEnter(this);
        }
        Debug.Log("New state " + _currentState.name);
    }

    // Este metodo es llamado con un evento tras finalizar la animacion de ataque
    private void Die()
    {
        GoToState<DieState>();
    }

    // Detecta si el objetivo esta dentro del rango de vision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Target.tag))
        {
            GoToState<ChaseState>();
        }
    }

    // Detecta si el objetivo sale del rango de vision
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Target.tag))
        {
            GoToState<IdleState>();
        }
    }

    // Detecta la colision con la bomba
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with target");

        if (collision.gameObject.CompareTag(Target.tag))
        {
            GoToState<AttackState>();
        }
    }
}
