
using UnityEngine;

public abstract class EnemySO : EntitySO
{
    [SerializeField] private float attackRange;
    public float AttackRange { get => attackRange; }

    public abstract void Attack();
}
