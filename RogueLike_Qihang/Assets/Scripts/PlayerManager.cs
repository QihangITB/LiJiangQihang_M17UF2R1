using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour, InputControl.IPlayerActions
{
    const string AxisX = "X", AxisY = "Y";
    const string ParamIsMoving = "IsMoving";
    const string WeaponObject = "Weapon";

    const float StopSpeed = 0f;

    public bool BlockPlayer { get; set; } = false;
    public Vector2 PlayerDirection { get; private set; }

    [SerializeField] private EntitySO _playerData;
    [SerializeField] private GameObject _hudMenu;
    [SerializeField] private GameObject _inventoryMenu;

    private InputControl _inputControl;
    private Vector2 _inputMovement;
    private Animator _animator;
    private Movement _movement;
    private InventoryManager _inventoryManager;

    void Awake()
    {
        InitializeComponents();
    }

    void Update()
    {
        AnimationByDirection();

        // Actualizamos la direccion constantemente
        _movement.SetDirection(PlayerDirection);
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
        _movement.SetSpeed(_playerData.Speed);

        _inventoryManager = GetComponent<InventoryManager>();
    }

    private void AnimationByDirection()
    {
        _animator.SetFloat(AxisX, PlayerDirection.x);
        _animator.SetFloat(AxisY, PlayerDirection.y);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (!BlockPlayer) 
        {
            _inputMovement = context.ReadValue<Vector2>();
            PlayerDirection = _inputMovement.normalized;

            bool isMoving = _inputMovement.magnitude > StopSpeed;
            _animator.SetBool(ParamIsMoving, isMoving);
        }
        else
        {
            PlayerDirection = Vector2.zero;
            _animator.SetBool(ParamIsMoving, false);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && !BlockPlayer)
        {
            GameObject weapon = GameObject.Find(WeaponObject);
            weapon.GetComponent<WeaponManager>().Attack();
        }
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _hudMenu.SetActive(!_hudMenu.activeSelf);
            _inventoryMenu.SetActive(!_inventoryMenu.activeSelf);

            // Bloqueamos al jugador si el inventario esta activo
            BlockPlayer = _inventoryMenu.activeSelf; 
        }
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
        {
            // Cambiamos de arma desde el manager de inventario
            _inventoryManager.ChangeWeapon();

            // Actualizamos la UI del HUD
            SlotManager slotManager = _hudMenu.GetComponent<SlotManager>();
            slotManager.UpdateSlotUI();
        }
    }
}
