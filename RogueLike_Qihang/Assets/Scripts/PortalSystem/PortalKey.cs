using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalKey : MonoBehaviour
{
    private const string PlayerTag = "Player";

    public static event Action OnKeyCollected;

    private void Update()
    {
        transform.Rotate(Vector3.forward, 1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PlayerTag))
        {
            OnKeyCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}
