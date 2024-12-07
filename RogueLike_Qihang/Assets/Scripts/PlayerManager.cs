using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour, InputControl.IPlayerActions
{
    const string AxisX = "X", AxisY = "Y";
    const string ParamSpeed = "Speed", ParamIsMoving = "isMoving";

    const float StopSpeed = 0f;

    public static Vector2 PlayerDirection { get; private set; }

    private InputControl _inputControl;
    private Vector2 _inputMovement;
    private Animator _animator;


    void Awake()
    {
        InitializeComponents();
    }

    void Update()
    {
        AnimationByDirection();
        MovingAnimation();
    }

    void OnEnable()
    {
        _inputControl.Enable();
    }

    void OnDisable()
    {
        _inputControl.Disable();
    }
    private void InitializeComponents()
    {
        _animator = GetComponent<Animator>();
        _inputControl = new InputControl();
        _inputControl.Player.SetCallbacks(this);
    }

    private void AnimationByDirection()
    {
        _animator.SetFloat(AxisX, PlayerManager.PlayerDirection.x);
        _animator.SetFloat(AxisY, PlayerManager.PlayerDirection.y);
    }

    private void MovingAnimation()
    {
        bool isMoving = PlayerPrefs.GetFloat(ParamSpeed) > StopSpeed;
        _animator.SetBool(ParamIsMoving, isMoving);
    }
    public void OnMovement(InputAction.CallbackContext context)
    { 
        _inputMovement = context.ReadValue<Vector2>();
        PlayerDirection = _inputMovement.normalized;

        bool isMoving = _inputMovement.magnitude > StopSpeed;
        PlayerPrefs.SetFloat(ParamSpeed, isMoving ? 2f : StopSpeed);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            Debug.Log("Attack! " + context.control.name);
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
            Debug.Log("Open Inventory! " + context.control.name);
    }

    public void OnConsumable(InputAction.CallbackContext context)
    {
        if (context.performed)
            Debug.Log("Use Consumable! " + context.control.name);
    }

    public void OnEquipment(InputAction.CallbackContext context)
    {
        if (context.performed)
            Debug.Log("Change equipment! " + context.control.name);
    }
}
