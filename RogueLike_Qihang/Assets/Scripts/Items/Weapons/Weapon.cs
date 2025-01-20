using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public WeaponSO WeaponData;
    public GameObject WeaponPrefab;
    protected override ItemSO ItemData => WeaponData;

    public void UseWeapon(GameObject dynamicPrefab)
    {
        // Pasamos el prefab dinamico en lugar del estatico
        WeaponData.UseWeapon(dynamicPrefab);
    }
}
