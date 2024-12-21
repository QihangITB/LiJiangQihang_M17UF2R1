using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Launcher", menuName = "Weapon/Launcher")]
public class Launcher : WeaponSO
{
    private const string ParamShoot = "Shoot";

    [SerializeField] private GameObject _grenadePrefab;

    public override void UseWeapon()
    {

    }

    private void Shoot()
    {
        Transform spawnPoint = WeaponPrefab.transform.GetChild(0);
        GameObject grenade = Instantiate(_grenadePrefab, spawnPoint.position, Quaternion.identity);
    }
}
