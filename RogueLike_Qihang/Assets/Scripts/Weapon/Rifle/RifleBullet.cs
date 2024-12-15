using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class RifleBullet : MonoBehaviour
{
    private Animator _animator;
    private Movement _movement;

    void Start()
    {
        _animator = GetComponent<Animator>();
        SetMovementConfiguration();
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(GetMouseDirection().y, GetMouseDirection().x) * Mathf.Rad2Deg);
    }

    public void SetMovementConfiguration()
    {
        _movement = GetComponent<Movement>();
        _movement.SetSpeed(GetCurrentWeaponSpeed()); // Damos per hecho que tiene equipado el rifle.
        _movement.SetDirection(GetMouseDirection());
    }

    private Vector2 GetMouseDirection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (mousePosition - (Vector2)transform.position).normalized;
    }

    private float GetCurrentWeaponSpeed()
    {
        GameObject weapon = GameObject.Find("WeaponManager");
        WeaponManager weaponManager = weapon.GetComponent<WeaponManager>();
        return weaponManager.CurrentWeapon.Speed;
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        _animator.SetTrigger("Hit");
    }

    // Funcion que se llama al final de la animación de impacto a traves de un evento
    private void DestroyBullet()
    {
        RifleMunition.pushBullet(this.gameObject);
    }
}
