using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationManager : MonoBehaviour
{
    const string AxisX = "X", AxisY = "Y";
    const string ParamIsMoving = "isMoving", ParamSpeed = "Speed";

    const float StopSpeed = 0f, PositionCooldown = 1f;

    private Animator _animator;
    private Vector2 _savePosition;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(SavePositionByTime());
    }

    void Update()
    {
        AnimationByDirection();
        MovingAnimation();
    }

    //La animacion de movimiento dependera del input del teclado
    private void AnimationByDirection()
    {
        _animator.SetFloat(AxisX, GetDirection().x);
        _animator.SetFloat(AxisY, GetDirection().y);
    }

    private Vector2 GetDirection()
    {
        return ((Vector2) transform.position - _savePosition).normalized;
    }

    IEnumerator SavePositionByTime()
    {
        while(true)
        {
            if(PlayerPrefs.GetFloat(ParamSpeed) > StopSpeed)
                _savePosition = transform.position;
            yield return new WaitForSeconds(PositionCooldown);
        }
    }

    private void MovingAnimation()
    {
        bool isMoving = PlayerPrefs.GetFloat(ParamSpeed) > StopSpeed;
        _animator.SetBool(ParamIsMoving, isMoving);
    }
}
