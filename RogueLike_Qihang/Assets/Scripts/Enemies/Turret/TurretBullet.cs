using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    private const string PlayerTag = "Player";
    private GameObject _target;
    private Movement _movement;
    private float _speed;
    private Vector2 _direction;

    public float Speed { get => _speed; set => _speed = value; }
    public Vector2 Direction { get => _direction; set => _direction = value; }

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag(PlayerTag);

        if (!HasATarget(_direction, _target.transform))
        {
            Destroy(gameObject);
            return;
        }

        _movement = GetComponent<Movement>();
        _movement.SetSpeed(_speed);
        _movement.SetDirection(_direction);
    }

    private bool HasATarget(Vector2 bulletDirection, Transform target)
    {
        Vector2 targetDirection = (target.position - transform.position).normalized;
        float angle = Vector2.SignedAngle(bulletDirection, targetDirection);
        return TurretAim.IsAimingTheTarget(angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit");
            Destroy(gameObject);
        }
    }
}
