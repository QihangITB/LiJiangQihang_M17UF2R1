using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "Enemy/Turret")]
public class Turret : EnemySO
{
    [SerializeField] private float attackSpeed; // Velocidad de las balas
    [SerializeField] private float rechargeCooldown; // Tiempo entre disparos
    [SerializeField] private GameObject bulletPrefab;

    public float AttackSpeed { get => attackSpeed; }
    public float RechargeCooldown { get => rechargeCooldown; }
    public GameObject BulletPrefab { get => bulletPrefab; }

    public override void Attack()
    {
        Debug.Log("Turret Attack");
    }
}
