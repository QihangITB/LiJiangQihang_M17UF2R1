using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const string ParamImpact = "Impact", EnemyTag = "Enemy", PlayerTag = "Player";

    [SerializeField] private Rifle _data;
    private Animator _animator;
    private Movement _movement;
    private Vector2 _direction;

    private void Awake()
    {
        InitializeComponents();
    }

    private void OnEnable()
    {
        Transform player = GameObject.FindGameObjectWithTag(PlayerTag).transform;
        _direction = WeaponManager.GetMouseDirection(player.transform);
        RotateTowardsMouse(_direction);
    }

    private void Update()
    {
        _movement.Move(_data.Speed, _direction);
    }

    private void InitializeComponents()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
    }

    private void RotateTowardsMouse(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Esta funcion se llama al final de la animacion de impacto, a traves de un evento en el animation
    private void DestroyBullet()
    {
        BulletMunition.PushBullet(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _animator.SetTrigger(ParamImpact);

        if (collision.gameObject.CompareTag(EnemyTag))
        {
            Debug.Log("Hit: " + collision.gameObject.name);
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(_data.Damage);
        }
    }
}
