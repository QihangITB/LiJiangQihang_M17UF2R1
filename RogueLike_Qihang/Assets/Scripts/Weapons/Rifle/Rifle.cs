using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rifle", menuName = "Weapon/Rifle")]
public class Rifle : WeaponSO
{
    const string ParamShoot = "Shoot";

    public BulletMunition munition;

    public override void Use()
    {
        GameObject bulletSpawner = WeaponPrefab.transform.GetChild(0).gameObject;
        DoSpawnAnimation(bulletSpawner);
        RechargeBullet(bulletSpawner);
    }

    private void DoSpawnAnimation(GameObject spawner)
    {
        Animator animator = spawner.GetComponent<Animator>();
        animator.SetTrigger(ParamShoot);
    }

    private void RechargeBullet(GameObject spawner)
    {
        munition.popBullet(spawner.transform);
    }
}
