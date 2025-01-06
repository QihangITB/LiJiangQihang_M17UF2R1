using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private const string ParamExplote = "Explote", EnemyTag = "Enemy";

    [SerializeField] private Launcher _data;
    private Animator _animator;
    private float exploteTimer;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        RotateTowardsMouse();
        exploteTimer = 0;
    }

    private void Update()
    {
        exploteTimer += Time.deltaTime;

        if (exploteTimer >= _data.ExploteTime)
        {
            _animator.SetTrigger(ParamExplote);
        }

    }

    private void RotateTowardsMouse()
    {
        Vector2 direction = WeaponManager.GetMouseDirection(this.transform);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // Esta funcion se llama al final de la animacion de explosion, a traves de un evento en el animation
    private void DestroyGrenade()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _animator.SetTrigger(ParamExplote);

        if (collision.gameObject.CompareTag(EnemyTag))
        {
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(_data.Damage);
        }
    }
}
