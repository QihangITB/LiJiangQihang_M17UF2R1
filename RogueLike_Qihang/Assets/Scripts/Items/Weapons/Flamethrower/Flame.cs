using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    private const string EnemyTag = "Enemy";

    [SerializeField] private Flamethrower _data;

    void OnParticleCollision(GameObject collision)
    {
        if(collision.CompareTag(EnemyTag))
        {
            collision.GetComponent<HealthManager>().TakeDamage(_data.Damage);
        }
    }
}
