using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour, InputControl.IPlayerActions
{
    private const string ParamX = "X", ParamY = "Y", ParamIsMoving = "IsMoving", ParamIsDead = "IsDead";
    private const string WeaponObject = "Weapon";
    private const string EndScene = "End";

    private const float StopSpeed = 0f;

    public bool BlockPlayer { get; set; } = false;

    [SerializeField] private EntitySO _playerData;
    [SerializeField] private GameObject _hudMenu;
    [SerializeField] private GameObject _inventoryMenu;

    private InputControl _inputControl;
    private Vector2 _inputMovement;
    private Vector2 _playerDirection;
    private Animator _animator;
    private AudioSource _audio;
    private Movement _movement;
    private InventoryManager _inventoryManager;
    private HealthManager _healthManager;

    void Awake()
    {
        InitializeComponents();
    }

    void Update()
    {
        if (_healthManager.IsDead)
        {
            _animator.SetBool(ParamIsDead, true);
            return;
        }

        // Aplicamos el movimiento al jugador
        _movement.Move(_playerData.Speed, _playerDirection);
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

        _audio = GetComponent<AudioSource>();

        _inputControl = new InputControl();
        _inputControl.Player.SetCallbacks(this);

        _movement = GetComponent<Movement>();

        _inventoryManager = InventoryManager.Instance;

        _healthManager = GetComponent<HealthManager>();
    }

    private void AnimationByDirection(Vector2 direction)
    {
        _animator.SetFloat(ParamX, direction.x);
        _animator.SetFloat(ParamY, direction.y);
    }

    // Funcion llamada por el evento de la animacion de muerte
    public void GameOver()
    {
        SceneController.Instance.LoadSceneByName(EndScene);
    }

    // Funcion llamada por el evento de la animacion de muerte
    private void PlayDeathSound()
    {
        if (_audio != null)
        {
            _audio.PlayOneShot(_playerData.DeathSound); ;
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed && !BlockPlayer)
        {
            _inputMovement = context.ReadValue<Vector2>();
            _playerDirection = _inputMovement.normalized;

            bool isMoving = _inputMovement.magnitude > StopSpeed;
            _animator.SetBool(ParamIsMoving, isMoving);

            AnimationByDirection(_playerDirection);
        }
        else if (context.canceled || BlockPlayer)
        {
            _playerDirection = Vector2.zero;
            _animator.SetBool(ParamIsMoving, false);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && !BlockPlayer)
        {
            GameObject weapon = GameObject.Find(WeaponObject);

            // La funcion de atacar lo hace solo si tiene una instancia de arma
            if(weapon.transform.childCount != 0)
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

    public void OnEquipment(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Cambiamos de arma desde el manager de inventario
            // Se nos activara el evento para cambiar la UI
            _inventoryManager.ChangeWeapon();
        }
    }

    // IMPLEMENTAR POWER UPS SI HAY TIEMPO
    public void OnConsumable(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Funcion temporal para mostrar mensaje
            ShowNotDone msg = GetComponent<ShowNotDone>();
            string inputValue = context.control.displayName;
            StopCoroutine(msg.ActiveMessage(inputValue.ToString()));
            StartCoroutine(msg.ActiveMessage(inputValue.ToString()));
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SceneController.Instance.LoadPause();
        }
    }
}
