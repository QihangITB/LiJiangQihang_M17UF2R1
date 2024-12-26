using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "Weapon/Sword")]
public class Sword : WeaponSO
{
    const string ParamBrandish = "Brandish";
    public override void Use()
    {
        GetSwordEdge().ActiveEdge(); // Activamos el collider del filo
        Animator animator = WeaponPrefab.GetComponent<Animator>();
        animator.SetTrigger(ParamBrandish);
    }

    private SwordEdge GetSwordEdge()
    {
        return WeaponPrefab.GetComponent<SwordEdge>();
    }
}
