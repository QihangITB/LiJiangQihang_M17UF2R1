using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEdge : MonoBehaviour
{
    private BoxCollider2D _edgeCollider;

    private void Awake()
    {
        _edgeCollider = GetComponent<BoxCollider2D>();
        _edgeCollider.enabled = false; // Desactivamos el collider al inicio
    }

    public void ActiveEdge()
    {
        // Se activa cuando atacamos
        _edgeCollider.enabled = true;
    }

    public void DeactiveEdge()
    {
        // Se desactiva cuando se termina la animacion de ataque
        _edgeCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO:
        // Hacer daño al enemigo
        Debug.Log("Hit: " + collision.gameObject.name);
    }
}
