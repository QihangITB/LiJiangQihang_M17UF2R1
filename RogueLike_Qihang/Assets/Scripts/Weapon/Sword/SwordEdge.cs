using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEdge : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO:
        // Hacer daño al enemigo
        Debug.Log("Hit: " + collision.gameObject.name);
    }
}
