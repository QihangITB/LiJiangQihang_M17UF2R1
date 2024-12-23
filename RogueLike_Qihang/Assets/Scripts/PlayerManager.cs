using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour, InputControl.IPlayerActions
{
    const string AxisX = "X", AxisY = "Y";
    const string ParamIsMoving = "IsMoving";
    const string WeaponManager = "WeaponManager";

    const float DefaultSpeed= 1f, StopSpeed = 0f;

    public Vector2 playerDirection { get; private set; }

    private InputControl _inputControl;
    private Vector2 _inputMovement;
    private Animator _animator;
    private Movement _movement;
    private Inventory _inventory;

    void Awake()
    {
        InitializeComponents();
    }

    void Update()
    {
        AnimationByDirection();
        _movement.SetDirection(playerDirection); // Actualizamos la direccion constantemente
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

        _movement = GetComponent<Movement>();
        _movement.SetSpeed(DefaultSpeed);

        _inventory = GetComponent<Inventory>();
    }

    private void AnimationByDirection()
    {
        _animator.SetFloat(AxisX, playerDirection.x);
        _animator.SetFloat(AxisY, playerDirection.y);
    }

    public void OnMovement(InputAction.CallbackContext context)
    { 
        _inputMovement = context.ReadValue<Vector2>();
        playerDirection = _inputMovement.normalized;

        bool isMoving = _inputMovement.magnitude > StopSpeed;
        _animator.SetBool(ParamIsMoving, isMoving);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameObject weapon = GameObject.Find(WeaponManager);
            weapon.GetComponent<WeaponManager>().Attack();
        }
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
            Debug.Log("Open Inventory! " + context.control.name);
    }

    public void OnConsumable(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Use Consumable! " + context.control.name);
        }
    }

    public void OnEquipment(InputAction.CallbackContext context)
    {
        if (context.performed)
            Debug.Log("Change equipment! " + context.control.name);
    }
}
