using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "Weapon/Sword")]
public class Sword : WeaponSO
{
    const string ParamBrandish = "Brandish";
    public override void UseWeapon(GameObject weapon)
    {
        GetSwordEdge(weapon).ActiveEdge(); // Activamos el collider del filo
        Animator animator = weapon.GetComponent<Animator>();
        animator.SetTrigger(ParamBrandish);
    }

    private SwordEdge GetSwordEdge(GameObject sword)
    {
        return sword.GetComponent<SwordEdge>();
    }
}
