using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bomber", menuName = "Enemy/Bomber")]
public class Bomber : EnemySO
{
    public override void Attack()
    {
        Debug.Log("Bomber Attack");
    }
}
