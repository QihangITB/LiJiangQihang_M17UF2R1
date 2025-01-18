using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Move(float speed, Vector2 direction)
    {
        _rb.MovePosition(_rb.position + (speed * direction * Time.deltaTime));
    }
}
