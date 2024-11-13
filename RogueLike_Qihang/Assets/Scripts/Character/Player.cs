using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : AEntity, InputControl.IPlayerActions
{
    [SerializeField] private float speed;
    private InputControl _inputControl;
    private Vector2 _inputMovement;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _inputControl = new InputControl();
        _inputControl.Player.SetCallbacks(this);
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _rb.MovePosition(_rb.position + speed * Time.deltaTime * _inputMovement.normalized);
    }

    private void OnEnable()
    {
        _inputControl.Player.Enable();
    }

    private void OnDisable()
    {
        _inputControl.Player.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        //Leemos la configuracion de las teclas de movimiento. EX: 'w' = (0,1)
        _inputMovement = context.ReadValue<Vector2>();
        Debug.Log(_inputMovement);
    }

}
