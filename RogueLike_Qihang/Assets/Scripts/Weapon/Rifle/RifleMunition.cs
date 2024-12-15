using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RifleMunition
{
    public GameObject bulletPrefab;

    public static Stack<GameObject> bulletsPool;

    public static void InitializeMunitionStack()
    {
        bulletsPool = new Stack<GameObject>();
    }

    public static void pushBullet(GameObject bullet)
    {
        bulletsPool.Push(bullet);
        bullet.SetActive(false);
    }

    private GameObject CreateBullet()
    {
        return Object.Instantiate(bulletPrefab);
    }

    public GameObject popBullet(Transform spawn)
    {
        GameObject bullet = bulletsPool.Count > 0 ? bulletsPool.Pop() : CreateBullet();
        bullet.transform.position = spawn.position;
        bullet.SetActive(true);

        return bullet;
    }

}
