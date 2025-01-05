
using UnityEngine;

public abstract class EnemySO : EntitySO
{
    [SerializeField] private float visionRange;
    public float VisionRange { get => visionRange; }
}
