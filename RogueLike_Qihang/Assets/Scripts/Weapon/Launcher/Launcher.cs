using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Launcher", menuName = "Weapon/Launcher")]
public class Launcher : WeaponSO
{
    [SerializeField] private GameObject _grenadePrefab;

    public override void UseWeapon()
    {
        Transform spawnPoint = WeaponPrefab.transform.GetChild(0);
        GameObject grenade = Instantiate(_grenadePrefab);
        grenade.transform.position = spawnPoint.position;
        Debug.Log("Grenade Launched: " + grenade.transform.position);
    }
}
