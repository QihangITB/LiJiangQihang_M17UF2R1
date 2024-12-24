using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private const int EquippedWeaponSlot = 2;
    public static InventoryManager Instance;

    private WeaponSO[] _equippedWeapons = new WeaponSO[EquippedWeaponSlot];
    private List<WeaponSO> _inventoryWeapons = new List<WeaponSO>();

    //TODO: Si da tiempo
    //public List<ConsumableSO> equippedConsumables;
    //public List<ConsumableSO> inventoryConsumables;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EquipWeapon(WeaponSO weapon, int slot)
    {
        _equippedWeapons[slot] = weapon;
    }

    public void AddWeapon(WeaponSO weapon)
    {
        _inventoryWeapons.Add(weapon);
    }

    public void RemoveWeapon(WeaponSO weapon)
    {
        _inventoryWeapons.Remove(weapon);
    }
}
