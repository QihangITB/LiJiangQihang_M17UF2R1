using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBullet : MonoBehaviour
{
    public float Speed { get; set; }
    public Animator _animator;

    void Start()
    {
        Speed = 2f;
    }

    void Update()
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        _animator.SetTrigger("Hit");
    }

    // Funcion que se llama al final de la animaci�n de impacto a traves de un evento
    private void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
