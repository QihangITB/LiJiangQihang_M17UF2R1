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
    private Vector2 _lastInput;

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
        AnimationByDirection();
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

        _animator.SetBool(ParamIsMoving, _inputMovement != Vector2.zero);
        if(_inputMovement != Vector2.zero)
            SaveLastInput();
    }

    //La animacion de movimiento dependera del input del teclado
    private void AnimationByDirection()
    {
        _animator.SetFloat(AxisX, _lastInput.x);
        _animator.SetFloat(AxisY, _lastInput.y);
    }

    private void SaveLastInput()
    {
        _lastInput = _inputMovement;
    }

}
