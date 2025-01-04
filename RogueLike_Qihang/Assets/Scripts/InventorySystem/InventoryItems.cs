using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItems : MonoBehaviour
{
    [SerializeField] private List<GameObject> _allItems;
    [SerializeField] private List<GameObject> _weaponsPrefabs;

    public List<GameObject> AllItems { get => _allItems; }
    public List<GameObject> WeaponsPrefabs { get => _weaponsPrefabs; }


}
