using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementManager : MonoBehaviour, InputControl.IPlayerActions
{
    const string ParamSpeed = "Speed";

    const float StopSpeed = 0f, PositionCooldown = 1.5f;

    public static Vector2 PlayerDirection { get; private set; }

    [SerializeField] private float speed;
    private InputControl _inputControl;
    private Vector2 _inputMovement;
    private Rigidbody2D _rb;
    private Vector2 _savePosition;


    void Awake()
    {
        _inputControl = new InputControl();
        _inputControl.Player.SetCallbacks(this);
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rb.MovePosition(_rb.position + PlayerPrefs.GetFloat(ParamSpeed) * Time.deltaTime * _inputMovement.normalized);
    }

    void OnEnable()
    {
        _inputControl.Enable();
    }

    void OnDisable()
    {
        _inputControl.Disable();
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _inputMovement = context.ReadValue<Vector2>();
            PlayerPrefs.SetFloat(ParamSpeed, speed);
            PlayerDirection = _savePosition - (Vector2)transform.position;
        }
        else if (context.canceled)
        {
            _inputMovement = Vector2.zero;
            PlayerPrefs.SetFloat(ParamSpeed, StopSpeed);
            _savePosition = transform.position;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {

    }

    public void OnInventory(InputAction.CallbackContext context)
    {

    }

    public void OnConsumable(InputAction.CallbackContext context)
    {

    }

    public void OnEquipment(InputAction.CallbackContext context)
    {

    }
}
