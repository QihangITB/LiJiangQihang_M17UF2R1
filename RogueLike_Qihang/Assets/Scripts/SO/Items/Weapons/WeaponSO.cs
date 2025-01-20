using UnityEngine;

public abstract class WeaponSO : ItemSO
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;

    public float Damage { get => damage; }
    public float Speed { get => speed; }

    public abstract void UseWeapon(GameObject weaponPrefab);
}
