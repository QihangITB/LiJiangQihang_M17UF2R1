using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "Enemy/Turret")]

public class Turret : EnemySO
{
    [SerializeField] private float attackSpeed;
    public float AttackSpeed { get => attackSpeed; }
}
