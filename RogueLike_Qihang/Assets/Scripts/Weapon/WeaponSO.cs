using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponSO : ScriptableObject
{
    [SerializeField] private float damage;
    [SerializeField] private float cost;
    [SerializeField] private string description;
    [SerializeField] private float speed;
    [SerializeField] public GameObject Weapon;

    public float Damage { get => damage; }
    public float Cost { get => cost; }
    public string Description { get => description; }
    public float Speed { get => speed; }

    public abstract void UseWeapon();
}
