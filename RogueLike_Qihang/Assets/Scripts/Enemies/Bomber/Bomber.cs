using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bomber", menuName = "Enemy/Bomber")]
public class Bomber : EnemySO
{
    [SerializeField] private float explosionRange;
    public float ExplosionRange { get => explosionRange; }
    public override void Attack()
    {
        Debug.Log("Bomber Attack");
    }
}
