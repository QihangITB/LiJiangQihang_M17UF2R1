using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bomber", menuName = "Entity/Enemy/Bomber")]
public class Bomber : EnemySO
{
    [SerializeField] private float explosionRange;
    public float ExplosionRange { get => explosionRange; }
}
