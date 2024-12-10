using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rifle", menuName = "Weapon/Rifle")]
public class Rifle : WeaponSO
{
    public GameObject bulletPrefab;
    
    private Stack<GameObject> bulletsPool;
    public override void UseWeapon()
    {
        Shoot();
    }

    private void Shoot()
    {

    }

    private void CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.SetActive(false);
        bulletsPool.Push(bullet);
    }

    public void PushBullet(GameObject bullet)
    {
        if (bulletsPool.Count <= 0)
        {
            bulletsPool.Push(bullet);
            bullet.SetActive(false);
        }
    }

    private GameObject PopBullet()
    {
        if (bulletsPool.Count > 0)
        {
            GameObject bullet = bulletsPool.Pop();
            bullet.SetActive(true);
            return bullet;
        }
        return null;
    }
}
