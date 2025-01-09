using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class AimBehaviour : MonoBehaviour
{
    public delegate void TriggerEvent(Collider2D collider);
    public event TriggerEvent OnTriggerStay; // Evento para cuando el jugador este dentro del trigger
    public event TriggerEvent OnTriggerExit; // Evento para cuando salga del trigger

    private GameObject _target;
    private bool _isTargetDetected = false;

    public GameObject Target { set => _target = value; }
    public bool IsTargetDetected { get => _isTargetDetected; }

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
            _isTargetDetected = IsTargetInLineOfSight(collision.transform);
            OnTriggerStay?.Invoke(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(_target.tag))
        {
            _isTargetDetected = false;
            OnTriggerExit?.Invoke(collision);
        }
    }
}
