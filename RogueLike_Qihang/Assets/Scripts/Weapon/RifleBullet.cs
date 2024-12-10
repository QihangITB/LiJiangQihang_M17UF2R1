using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBullet : MonoBehaviour
{
    public float Speed { get; set; }
    public Animator _animator;

    void Start()
    {
        Speed = GameObject.FindObjectOfType<Rifle>().Speed;
    }

    void Update()
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _animator.SetTrigger("Hit");
    }

    // Funcion que se llama al final de la animación de impacto a traves de un evento
    private void GoToPool()
    {
        Debug.Log("Bala enviado al pool");
        GameObject.FindObjectOfType<Rifle>().PushBullet(gameObject);
    }
}
