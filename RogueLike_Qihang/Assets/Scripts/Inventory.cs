using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public WeaponSO[] equippedWeapons;
    public WeaponSO[] inventoryWeapons;
    
    //TODO: Si da tiempo
    //public GameObject[] equippedPowerUps;
    //public GameObject[] inventoryPowerUps;

    public void EquipWeapon(WeaponSO selectedWeapon, int slotNumber)
    {
        equippedWeapons[slotNumber] = selectedWeapon;
    }
    
}
