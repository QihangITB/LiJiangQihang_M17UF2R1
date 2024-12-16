using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "Weapon/Sword")]
public class Sword : WeaponSO
{
    const string ParamBrandish = "Brandish";
    public override void UseWeapon()
    {
        Brandish();
    }

    private void Brandish()
    {
        Animator animator = WeaponPrefab.GetComponent<Animator>();
        animator.SetTrigger(ParamBrandish);
    }
}
