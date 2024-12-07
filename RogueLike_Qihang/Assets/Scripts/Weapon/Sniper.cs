using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sniper", menuName = "Weapon/Sniper")]
public class Sniper : WeaponSO
{
    public override void UseWeapon()
    {
        Debug.Log("Sniper shot");
    }
}
