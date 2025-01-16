using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    private const string PlayerTag = "Player";

    [SerializeField] private Turret _data;
    private GameObject _target;
    private Movement _movement;
    private float _speed;
    private Vector2 _direction;

    // Para poder assignar valores a los atributos privados
    public float Speed { get => _speed; set => _speed = value; }
    public Vector2 Direction { get => _direction; set => _direction = value; }

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag(PlayerTag);

        // Comprobamos que las balas tienen un objetivo, sino se eliminan
        if (!HasATarget(_direction, _target.transform))
        {
            Destroy(gameObject);
            return;
        }

        _movement = GetComponent<Movement>();
    }

    private void Update()
    {
        _movement.Move(_speed, _direction);
    }

    private bool HasATarget(Vector2 bulletDirection, Transform target)
    {
        Vector2 targetDirection = (target.position - transform.position).normalized;
        float angle = Vector2.SignedAngle(bulletDirection, targetDirection);
        return AimBehaviour.IsAimingTheTarget(angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PlayerTag))
        {
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(_data.Damage);
            Destroy(gameObject);
        }
    }
}
