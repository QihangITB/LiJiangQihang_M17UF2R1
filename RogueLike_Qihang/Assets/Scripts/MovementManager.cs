using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementManager : MonoBehaviour, InputControl.IPlayerActions
{
    const string PlayerTag = "Player";
    const string ParamSpeed = "Speed";

    const float StopSpeed = 0f;

    [SerializeField] private float speed;
    private InputControl _inputControl;
    private Vector2 _inputMovement;
    private Rigidbody2D _rb;

    void Awake()
    {
        _inputControl = gameObject.CompareTag(PlayerTag) ? new InputControl() : null;
        _inputControl.Player.SetCallbacks(this);
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (_inputControl != null)
            _rb.MovePosition(_rb.position + PlayerPrefs.GetFloat(ParamSpeed) * Time.deltaTime * _inputMovement.normalized);
    }

    void OnEnable()
    {
        if (_inputControl != null)
            _inputControl.Enable();
    }

    void OnDisable()
    {
        if (_inputControl != null)
            _inputControl.Disable();
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _inputMovement = context.ReadValue<Vector2>();
            PlayerPrefs.SetFloat(ParamSpeed, speed);
        }
        else if (context.canceled)
        {
            _inputMovement = Vector2.zero;
            PlayerPrefs.SetFloat(ParamSpeed, StopSpeed);
        }
    }
}
