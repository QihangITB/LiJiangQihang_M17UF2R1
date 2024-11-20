using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationManager : MonoBehaviour
{
    const string AxisX = "X", AxisY = "Y";
    const string ParamIsMoving = "isMoving", ParamSpeed = "Speed";

    const float StopSpeed = 0f;

    private Animator _animator;
    private Vector2 _mousePosition;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _mousePosition = GetMousePosition() - (Vector2)transform.position; //Posicion del raton respecto al jugador.
        AnimationByDirection();
        MovingAnimation();
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

    private void MovingAnimation()
    {
        bool isMoving = PlayerPrefs.GetFloat(ParamSpeed) > StopSpeed;
        _animator.SetBool(ParamIsMoving, isMoving);
    }
}
