using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : AEntity, InputControl.IPlayerActions
{
    const string AxisX = "X", AxisY = "Y", ParamIsMoving = "isMoving";

    [SerializeField] private float speed;

    private InputControl _inputControl;
    private Vector2 _inputMovement;
    private Rigidbody2D _rb;
    private Animator _animator;
    private Vector2 _mousePosition;

    private void Awake()
    {
        _inputControl = new InputControl();
        _inputControl.Player.SetCallbacks(this);
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        _rb.MovePosition(_rb.position + speed * Time.deltaTime * _inputMovement.normalized);
        _mousePosition = GetMousePosition() - (Vector2) transform.position; //Posicion del raton respecto al jugador.
        AnimationByDirection();
    }

    private void OnEnable()
    {
        _inputControl.Enable();
    }

    private void OnDisable()
    {
        _inputControl.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Leemos la configuracion de las teclas de movimiento. EX: 'w' = (0,1)
            _inputMovement = context.ReadValue<Vector2>();
            Debug.Log(_inputMovement);

            _animator.SetBool(ParamIsMoving, true);
        }
        else if (context.canceled)
        {
            _inputMovement = Vector2.zero;
            _animator.SetBool(ParamIsMoving, false);
        }
    }

    //Obtenemos la posicion del raton.
    private Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    //La animacion de movimiento dependera del input del teclado
    private void AnimationByDirection()
    {
        _animator.SetFloat(AxisX, _mousePosition.x);
        _animator.SetFloat(AxisY, _mousePosition.y);
    }
}
