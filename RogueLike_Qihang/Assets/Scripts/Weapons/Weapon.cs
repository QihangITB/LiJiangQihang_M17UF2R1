using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponSO WeaponData;
    public GameObject WeaponPrefab;

    public void UseWeapon(GameObject dynamicPrefab)
    {
        // Pasamos el prefab dinamico en lugar del estatico
        WeaponData.UseWeapon(dynamicPrefab);
    }
}
