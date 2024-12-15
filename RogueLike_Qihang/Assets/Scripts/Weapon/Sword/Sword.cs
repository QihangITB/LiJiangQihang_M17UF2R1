using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "Weapon/Sword")]
public class Sword : WeaponSO
{
    public override void UseWeapon()
    {
        Brandish();
    }

    private void Brandish()
    {

    }
}
