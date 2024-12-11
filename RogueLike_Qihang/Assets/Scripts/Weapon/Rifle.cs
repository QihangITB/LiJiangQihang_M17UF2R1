using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rifle", menuName = "Weapon/Rifle")]
public class Rifle : WeaponSO
{
    public GameObject bulletPrefab;

    public override void UseWeapon()
    {
        Shoot();
    }

    private void Shoot(Vector2 position)
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = position;
    }
}
