using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBehaviour : MonoBehaviour
{
    private GameObject _target;
    private bool _isTargetDetected = false;

    public GameObject Target { set => _target = value; }
    public bool TargetDetected { get => _isTargetDetected; }
    public GameObject DetectedTarget { get; set; }

    private void Update()
    {
        if(!_isTargetDetected)
        {
            DetectedTarget = null;
        }
    }

    public static bool IsAimingTheTarget(float angle)
    {
        return Mathf.Abs(angle) < 1f; // Verificar si está alineado con una tolerancia de 1 grado
    }

    private bool IsTargetInLineOfSight(Transform targetObject)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetObject.position - transform.position);
        Debug.DrawRay(transform.position, targetObject.position - transform.position, Color.red);
        return hit.collider.CompareTag(_target.tag);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(_target.tag))
        {
            if (IsTargetInLineOfSight(collision.transform))
            {
                DetectedTarget = collision.gameObject;
                _isTargetDetected = true;
            }
            else
            {
                _isTargetDetected = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(_target.tag))
        {
            _isTargetDetected = false;
        }
    }
}
