using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class RifleBullet : MonoBehaviour
{
    private float _speed;
    private Vector2 _dir;
    public Animator _animator;

    void Start()
    {
        _speed = 2f;
        _dir = GetMouseDirection();
    }

    void Update()
    {
        transform.Translate(_dir * _speed * Time.deltaTime);
    }

    private Vector2 GetMouseDirection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (mousePosition - (Vector2)transform.position).normalized;
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
