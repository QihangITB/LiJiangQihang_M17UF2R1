using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour, InputControl.IPlayerActions
{
    private InputControl _inputControl;

    void Awake()
    {
        _inputControl = new InputControl();
        _inputControl.Player.SetCallbacks(this);
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
