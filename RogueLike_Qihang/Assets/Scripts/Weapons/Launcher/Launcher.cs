using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Launcher", menuName = "Item/Weapon/Launcher")]
public class Launcher : WeaponSO
{
    [SerializeField] private GameObject _grenadePrefab;

    public override void UseWeapon(GameObject weapon)
    {
        Transform spawnPoint = weapon.transform.GetChild(0);
        GameObject grenade = Instantiate(_grenadePrefab);
        grenade.transform.position = spawnPoint.position;
    }
}
