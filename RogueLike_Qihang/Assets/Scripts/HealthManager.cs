using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private EntitySO _entityData;

    private float _health;

    public float Health { get => _health; } // Solo lectura para mostrar al jugador
    public bool IsDead { get; private set; }

    private void Start()
    {
        _health = _entityData.Health;
    }
    public void TakeDamage(float damage)
    {
        _health -= damage;
        IsDead = _health <= 0;
    }
}
