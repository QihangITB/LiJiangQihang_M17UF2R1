using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rifle", menuName = "Weapon/Rifle")]
public class Rifle : WeaponSO
{
    public RifleMunition munition;

    public override void UseWeapon()
    {
        Shoot();
    }

    private void Shoot()
    {
        Transform spawner = Weapon.transform.GetChild(0);
        munition.popBullet(spawner); 
    }
}
