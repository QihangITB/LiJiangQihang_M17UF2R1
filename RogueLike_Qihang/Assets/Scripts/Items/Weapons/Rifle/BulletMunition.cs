using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletMunition
{
    public GameObject bulletPrefab;

    public static Stack<GameObject> bulletsPool;

    public static void InitializeMunitionStack()
    {
        bulletsPool = new Stack<GameObject>();
    }

    public static void PushBullet(GameObject bullet)
    {
        bulletsPool.Push(bullet);
        bullet.SetActive(false);
    }

    private GameObject CreateBullet()
    {
        return Object.Instantiate(bulletPrefab);
    }

    public GameObject PopBullet(Transform spawn)
    {
        GameObject bullet = bulletsPool.Count > 0 ? bulletsPool.Pop() : CreateBullet();
        bullet.transform.position = spawn.position;
        bullet.SetActive(true);

        return bullet;
    }

}
