using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponSO : ScriptableObject
{
    [SerializeField] private Sprite weaponSprite;
    [SerializeField] private float damage;
    [SerializeField] private float cost;
    [SerializeField] private string description;

    public Sprite WeaponSrpite { get => weaponSprite; }
    public float Damage { get => damage; }
    public float Cost { get => cost; }
    public string Description { get => description; }

    public abstract void UseWeapon();
}
