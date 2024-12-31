using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretAim : MonoBehaviour
{
    private const float MinRandomPoint = -1f, MaxRandomPoint = 1f;

    public GameObject Target;
    [SerializeField] private Turret _turretData;

    private CircleCollider2D _circleCollider;
    private float _rotationSpeed;
    private Vector3 _targetPosition;
    private bool _isTargetDetected = false;

    public Vector3 TargetPosition { get => _targetPosition; }
    public bool CanShoot { get; private set; } = false;

    private void Start()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _circleCollider.radius = _turretData.VisionRange;
        _rotationSpeed = _turretData.Speed;
        _targetPosition = GenerateRandomTarget();
    }

    private void Update()
    {
        Vector3 directionToTarget = (_targetPosition - transform.position).normalized;
        float angleToTarget = AngleToRotate(_targetPosition);

        RotateTowardsTarget(directionToTarget, angleToTarget, _rotationSpeed);

        if (IsAimingTheTarget(angleToTarget))
        {
            CanShoot = _isTargetDetected; // Solo podemos disparar si el objetivo está dentro del rango de visión

            if (!CanShoot)
            {
                // Cambiamos el objetivo si estamos apuntando el objetivo pero no podemos disparar
                _targetPosition = GenerateRandomTarget();
            }
        }

       Debug.DrawLine(transform.position, _targetPosition, Color.green);
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

    public static bool IsAimingTheTarget(float angle)
    {
        return Mathf.Abs(angle) < 1f; // Verificar si está alineado con una tolerancia de 1 grado
    }

    private float AngleToRotate(Vector3 targetPosition)
    {
        Vector2 frontDirection = transform.up;
        Vector2 directionToTarget = targetPosition - transform.position;

        // Devuelve el angulo entre dos vectores
        return Vector2.SignedAngle(frontDirection, directionToTarget);
    }

    private Vector3 GenerateRandomTarget()
    {
        float randomX = Random.Range(MinRandomPoint, MaxRandomPoint);
        float randomY = Random.Range(MinRandomPoint, MaxRandomPoint);

        Vector3 localTarget = new Vector3(randomX, randomY, 0);

        // Convertir el punto a espacio mundial y devolverlo
        return transform.TransformPoint(localTarget);
    }

    private bool IsTargetInLineOfSight(Transform targetObject)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetObject.position - transform.position);
        Debug.DrawRay(transform.position, targetObject.position - transform.position, Color.red);
        return hit.collider.CompareTag(Target.tag);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Target.tag))
        {
            if(IsTargetInLineOfSight(collision.transform))
            {
                _targetPosition = collision.transform.position;
                _isTargetDetected = true;
            }
            else
            {
                _isTargetDetected = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Target.tag))
        {
            _isTargetDetected = false;
        }
    }
}
