
using UnityEngine;

public abstract class EntitySO : ScriptableObject
{
    [SerializeField] private float health;
    [SerializeField] private float speed;

    public float Health { get => health; }
    public float Speed { get => speed; }
}
