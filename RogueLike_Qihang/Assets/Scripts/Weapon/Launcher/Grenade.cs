using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private const string ParamExplote = "Explote";

    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        RotateTowardsMouse();
    }

    private void RotateTowardsMouse()
    {
        Vector2 direction = WeaponManager.GetMouseDirection(this.transform);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //TODO
        // Hacer daño al enemigo
        Debug.Log("Hit: " + collision.gameObject.name);
        _animator.SetTrigger(ParamExplote);
    }
}
