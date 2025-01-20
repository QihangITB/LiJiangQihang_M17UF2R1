using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretController : MonoBehaviour
{
    private const string PlayerTag = "Player";
    private const float MinRandomPoint = -1f, MaxRandomPoint = 1f;

    [SerializeField] private Turret _turretData;
    public GameObject Target;

    private float _rotationSpeed;
    private Vector3 _targetPosition;
    private AimBehaviour _aim;
    private ShootBehaviour _shoot;
    private HealthManager _healthManager;
    private CoinManager _coinManager;

    public Vector3 TargetPosition { get => _targetPosition; }

    private void Start()
    {
        if (Target == null)
        {
            Target = GameObject.FindGameObjectWithTag(PlayerTag);
        }

        InitializeComponents();
    }

    private void Update()
    {
        if (!CheckIfIsAlive(_healthManager)) return;

        Vector3 directionToTarget = (_targetPosition - transform.position).normalized;
        float angleToTarget = AngleToRotate(_targetPosition);

        RotateTowardsTarget(directionToTarget, angleToTarget, _rotationSpeed);

        if (AimBehaviour.IsAimingTheTarget(angleToTarget))
        {
            if (_aim.IsTargetDetected)
            {
                // Disparar si el objetivo está detectado
                _shoot.HandleCooldown(Time.deltaTime);

                if (_shoot.CanShoot())
                {
                    _shoot.Shoot(_turretData, Target.transform.position);
                }
            }
            else
            {
                // Cambiar el objetivo si no se puede disparar
                _targetPosition = GenerateRandomTarget(MinRandomPoint, MaxRandomPoint);
            }
        }

        Debug.DrawLine(transform.position, _targetPosition, Color.green);
    }

    private void OnDestroy()
    {
        if (_aim != null)
        {
            _aim.OnTriggerStay -= TriggerStay2D;
        }
    }

    private void InitializeComponents()
    {
        GetComponent<CircleCollider2D>().radius = _turretData.VisionRange;
        _rotationSpeed = _turretData.Speed;

        _aim = GetComponent<AimBehaviour>();
        _aim.Target = Target;
        _aim.OnTriggerStay += TriggerStay2D;

        _shoot = GetComponent<ShootBehaviour>();

        _healthManager = transform.parent.GetComponent<HealthManager>();

        _coinManager = transform.parent.GetComponent<CoinManager>();

        _targetPosition = GenerateRandomTarget(MinRandomPoint, MaxRandomPoint);
    }
    private void RotateTowardsTarget(Vector3 direction, float angle, float rotationSpeed)
    {
        float step = rotationSpeed * Time.deltaTime;

        if (Mathf.Abs(angle) > step)
        {
            float rotationAngle = Mathf.Sign(angle) * step;
            transform.Rotate(0, 0, rotationAngle);
        }
        else
        {
            transform.up = direction; // Ajustar directamente si el ángulo es pequeño
        }
    }

    private float AngleToRotate(Vector3 targetPosition)
    {
        Vector2 frontDirection = transform.up;
        Vector2 directionToTarget = targetPosition - transform.position;

        // Devuelve el angulo entre dos vectores
        return Vector2.SignedAngle(frontDirection, directionToTarget);
    }

    private Vector3 GenerateRandomTarget(float min, float max)
    {
        float randomX = Random.Range(min, max);
        float randomY = Random.Range(min, max);

        Vector3 localTarget = new Vector3(randomX, randomY, 0);

        // Convertir el punto a espacio mundial y devolverlo
        return transform.TransformPoint(localTarget);
    }

    private bool CheckIfIsAlive(HealthManager health)
    {
        if (health.IsDead)
        {
            _coinManager.IncreasePlayerCoins(Target); // Increment player coins
            Destroy(transform.parent.gameObject); // Destroy the parent (base)
            return false;
        }
        return true;
    }

    private void TriggerStay2D(Collider2D collision)
    {
        if (_aim.IsTargetDetected)
        {
            _targetPosition = collision.transform.position;
        }
    }
}
