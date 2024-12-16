using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class RifleBullet : MonoBehaviour
{
    private const string ParamImpact = "Impact";
    private const string WeaponManager = "WeaponManager";

    private Animator _animator;
    private Movement _movement;

    void Awake()
    {
        InitializeComponents();

    }

    void OnEnable()
    {
        // Al reutilizar la bala, lo tenemos que configurar cada vez que se activa 
        ConfigureMovement();
        RotateTowardsMouse();
    }

    private void InitializeComponents()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
    }

    private void ConfigureMovement()
    {
        _movement.SetSpeed(GetCurrentWeaponSpeed());
        _movement.SetDirection(GetMouseDirection());
    }

    private void RotateTowardsMouse()
    {
        float angle = Mathf.Atan2(GetMouseDirection().y, GetMouseDirection().x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private Vector2 GetMouseDirection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (mousePosition - (Vector2)transform.position).normalized;
    }

    private float GetCurrentWeaponSpeed()
    {
        GameObject weapon = GameObject.Find(WeaponManager);
        WeaponManager weaponManager = weapon.GetComponent<WeaponManager>();
        return weaponManager.CurrentWeapon.Speed; // Asumimos que tiene equipado el rifle, y por ello, su velocidad.
    }

    // Esta funcion se llama al final de la animacion de impacto, a traves de un evento en el animation
    private void DestroyBullet()
    {
        RifleMunition.pushBullet(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _animator.SetTrigger(ParamImpact);
    }
}
