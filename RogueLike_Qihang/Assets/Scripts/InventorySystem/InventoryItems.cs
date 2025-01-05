using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItems : MonoBehaviour
{
    [SerializeField] private List<GameObject> _allItems;
    public List<GameObject> AllItems { get => _allItems; }

    public GameObject GetWeaponById(string id)
    {
        return _allItems.Find(item => item.GetComponent<Weapon>().WeaponData.Id == id);
    }
}
