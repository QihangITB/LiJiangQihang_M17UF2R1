using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessControl : MonoBehaviour
{
    private const string PlayerTag = "Player";
    private float delayTime = 0.5f;

    public GameObject Doors;
    public GameObject Enemies;

    private EdgeCollider2D _edgeCollider;

    private void Start()
    {
        _edgeCollider = GetComponent<EdgeCollider2D>();
        DeactivateRoom();
    }

    public void ConfigureAcces(bool value)
    {
        // Comprueba si se ha cogido bien el componente
        if (_edgeCollider == null)
            _edgeCollider = GetComponent<EdgeCollider2D>();

        _edgeCollider.enabled = value;
    }

    public void ActivateRoom()
    {
        Enemies.SetActive(true);
        Doors.SetActive(true);
    }

    public void DeactivateRoom()
    {
        Enemies.SetActive(false);
        Doors.SetActive(false);
    }

    private IEnumerator DelayedRoomActivation(float delay)
    {
        yield return new WaitForSeconds(delay);
        ActivateRoom();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerTag))
        {
            _edgeCollider.enabled = false;
            StartCoroutine(DelayedRoomActivation(delayTime));
        }
    }
}
