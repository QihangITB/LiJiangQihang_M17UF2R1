using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float _speed;
    private Vector2 _direction;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + (_speed * _direction * Time.deltaTime));
    }

    public void SetDirection(Vector2 value)
    {
        _direction = value;
    }

    public void SetSpeed(float value)
    {
        _speed = value;
    }
}
