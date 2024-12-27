using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    private const string WeaponTag = "Weapon", ConsumableTag = "Consumable";
    private const int InventoryMaxSize = 6;

    public static InventoryManager Instance;

    private List<ItemSO> _inventoryItems = new List<ItemSO>();
    private List<WeaponSO> _equippedWeapons = new List<WeaponSO> { null, null }; // Tamaño de 2 predefinido
    private List<ConsumableSO> _equippedConsumables = new List<ConsumableSO> { null, null, null }; // Tamaño de 3 predefinido

    public List<ItemSO> Items{ get => _inventoryItems; }
    public List<WeaponSO> Weapons { get => _equippedWeapons; }
    public List<ConsumableSO> Consumables { get => _equippedConsumables; }

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

    public void AddItem(ItemSO item)
    {
        if (_inventoryItems.Count < InventoryMaxSize)
        {
            _inventoryItems.Add(item);
            Debug.Log(item.name + " añadido con id: " + item.Id);
        }
        else
        {
            Debug.Log("Inventario esta lleno");
        }
    }

    public void RemoveItem(ItemSO item)
    {
        _inventoryItems.Remove(item);
    }

    public void Equip(GameObject slot, WeaponSO weapon)
    {
        int slotNumber = slot.transform.GetSiblingIndex();

        WeaponSO slotWeapon = _equippedWeapons[slotNumber];

        if (slotWeapon == null || slotWeapon.Id != weapon.Id)
        {
            _equippedWeapons[slotNumber] = weapon;
        }
    }

    public void Equip(GameObject slot, ConsumableSO consumable)
    {
        int slotNumber = slot.transform.GetSiblingIndex();
        ConsumableSO slotConsumable = _equippedConsumables[slotNumber];

        if (slotConsumable == null || slotConsumable.Id != consumable.Id)
        {
            _equippedConsumables[slotNumber] = consumable;
        }
    }

    public void Equip<T>(GameObject slot, T item) where T : ItemSO
    {
        int slotNumber = slot.transform.GetSiblingIndex();

        // Obtenemos la lista correspondiente según el tipo de item
        var slotItem = GetEquipment<T>(slotNumber);

        // Comprobamos si el slot está vacío o si el item es diferente al actual
        if (slotItem == null || slotItem.Id != item.Id)
        {
            SetEquipment(slotNumber, item);
        }
    }

    private T GetEquipment<T>(int slotNumber) where T : ItemSO
    {
        if (typeof(T) == typeof(WeaponSO))
        {
            return (T)(object)_equippedWeapons[slotNumber];
        }
        else if (typeof(T) == typeof(ConsumableSO))
        {
            return (T)(object)_equippedConsumables[slotNumber];
        }
        else
        {
            throw new InvalidOperationException("Tipo no soportado");
        }
    }

    private void SetEquipment<T>(int slotNumber, T item) where T : ItemSO
    {
        if (typeof(T) == typeof(WeaponSO))
        {
            _equippedWeapons[slotNumber] = (WeaponSO)(object)item;
        }
        else if (typeof(T) == typeof(ConsumableSO))
        {
            _equippedConsumables[slotNumber] = (ConsumableSO)(object)item;
        }
        else
        {
            throw new InvalidOperationException("Tipo no soportado");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int initialInventorySize = _inventoryItems.Count;

        if (collision.CompareTag(WeaponTag))
        {
            Weapon weapon = collision.GetComponent<Weapon>();
            AddItem(weapon.WeaponData);
        }
        else if (collision.CompareTag(ConsumableTag))
        {
            Consumable consumable = collision.GetComponent<Consumable>();
            AddItem(consumable.ConsumableData);
        }

        // Si ha variado el tamaño del inventario, destruimos el objeto porque se ha añadido
        if (initialInventorySize != _inventoryItems.Count)
        {
            Destroy(collision.gameObject);
        }
    }
}
