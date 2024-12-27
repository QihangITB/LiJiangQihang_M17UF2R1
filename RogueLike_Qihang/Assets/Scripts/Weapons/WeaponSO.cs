using UnityEngine;

public abstract class WeaponSO : ItemSO
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;

    [SerializeField] public GameObject WeaponPrefab;

    public float Damage { get => damage; }
    public float Speed { get => speed; }
}
