using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

public class BomberController : MonoBehaviour
{
    private const string PlayerTag = "Player";

    [SerializeField] private Bomber _bomberData;
    public Animator Animator;
    public GameObject Target;
    public List<StateSO> States;

    private StateSO _currentState;
    private CircleCollider2D _visionCollider;
    private CapsuleCollider2D _bombCollider;
    private NavMeshAgent _navMeshAgent;
    private AimBehaviour _aim;
    private HealthManager _healthManager;
    private CoinManager _coinManager;

    public NavMeshAgent Agent { get => _navMeshAgent; }

    private void Start()
    {
        if (Target == null)
        {
            Target = GameObject.FindGameObjectWithTag(PlayerTag);
        }

        InitializeComponents();

        _currentState = States[0];
        _currentState.OnStateEnter(this);
    }

    private void Update()
    {
        if (!CheckIfIsAlive(_healthManager)) return;

        _currentState.OnStateUpdate(this);
    }

    private void InitializeComponents()
    {
        _visionCollider = GetComponentInChildren<CircleCollider2D>();
        _visionCollider.radius = _bomberData.VisionRange;
        
        _bombCollider = GetComponent<CapsuleCollider2D>();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _bomberData.Speed;
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;

        _aim = GetComponent<AimBehaviour>();
        _aim.Target = Target;

        _healthManager = GetComponent<HealthManager>();

        _coinManager = GetComponent<CoinManager>();
    }

    private void GoToState<T>() where T : StateSO
    {
        if (_currentState.StatesToGo.Find(state => state is T))
        {
            _currentState.OnStateExit(this);
            _currentState = _currentState.StatesToGo.Find(obj => obj is T);
            _currentState.OnStateEnter(this);
        }
    }

    private bool CheckIfIsAlive(HealthManager health)
    {
        if (health.IsDead)
        {
            Debug.Log("Bomber is dead!");
            _coinManager.IncreasePlayerCoins(Target); // Aumentamos las monedas del jugador
            GoToState<AttackState>(); // Entra en modo ataque porque si muere, explota
            return false;
        }
        return true;
    }

    // Este metodo es llamado con un evento tras finalizar la animacion de ataque
    private void Die()
    {
        GoToState<DieState>();
    }

    // Detecta si el objetivo esta dentro del rango de vision
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_aim.IsTargetDetected && !(_currentState is ChaseState))
        {
            GoToState<ChaseState>();
        }
    }

    // Detecta si el objetivo sale del rango de vision
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Target.tag))
        {
            GoToState<IdleState>();
        }
    }

    // Detecta la colision con la bomba
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Target.tag))
        {
            // Convertimos en trigger para evitar empuje al personaje
            _bombCollider.isTrigger = true;

            // Ajustamos el tamaño de la colision para el jugador pueda recibir el daño estando "cerca"
            _bombCollider.size = new Vector2(_bomberData.ExplosionRange, _bomberData.ExplosionRange);

            collision.gameObject.GetComponent<HealthManager>().TakeDamage(_bomberData.Damage);

            GoToState<AttackState>();
        }
    }
}
