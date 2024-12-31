using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Bullet : MonoBehaviour
{
    private const string ParamImpact = "Impact";

    [SerializeField] private Rifle _rifle;
    private Animator _animator;
    private Movement _movement;

    void Awake()
    {
        InitializeComponents();
    }

    void OnEnable()
    {
        Vector2 mouseDirection = WeaponManager.GetMouseDirection(this.transform);
        // Al reutilizar la bala, lo tenemos que configurar cada vez que se activa 
        ConfigureMovement(_rifle, mouseDirection);
        RotateTowardsMouse(mouseDirection);
    }

    private void InitializeComponents()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
    }

    private void ConfigureMovement(Rifle weapon, Vector2 direction)
    {
        _movement.SetSpeed(weapon.Speed);
        _movement.SetDirection(direction);
    }

    private void RotateTowardsMouse(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Esta funcion se llama al final de la animacion de impacto, a traves de un evento en el animation
    private void DestroyBullet()
    {
        BulletMunition.pushBullet(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //TODO
        // Hacer daño al enemigo
        Debug.Log("Hit: " + collision.gameObject.name);
        _animator.SetTrigger(ParamImpact);
    }
}
