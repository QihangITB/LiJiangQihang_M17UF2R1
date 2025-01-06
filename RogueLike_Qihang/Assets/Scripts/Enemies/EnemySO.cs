
using UnityEngine;

public abstract class EnemySO : EntitySO
{
    [SerializeField] private float visionRange;
    [SerializeField] private float damage;
    public float VisionRange { get => visionRange; }
    public float Damage { get => damage; }
}
