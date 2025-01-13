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
    public event Action OnInventoryChanged;
    public event Action OnWeaponEquipped;
    public event Action OnWeaponUnequipped;

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
        }
    }

    public void RemoveItem(ItemSO item)
    {
        // Lo elimina de la lista de items
        _inventoryItems.Remove(item);

        // Si lo tiene equipado, tambien lo elimina
        if (_equippedWeapons.Contains(item))
        {
            _equippedWeapons.Remove((WeaponSO)item);
            OnWeaponUnequipped?.Invoke();
        }
        else if (_equippedConsumables.Contains(item))
        {
            _equippedConsumables.Remove((ConsumableSO)item);
        }

        OnInventoryChanged?.Invoke();
    }

    public void Equip<T>(GameObject slot, T item) where T : ItemSO
    {
        int slotNumber = slot.transform.GetSiblingIndex();
        ItemSO slotItem = GetEquipment<T>(slotNumber);

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
            OnWeaponEquipped?.Invoke();
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

    public void ChangeWeapon()
    {
        if (_equippedWeapons[1] != null)
        {
            WeaponSO aux = _equippedWeapons[0]; 
            OnWeaponUnequipped?.Invoke(); // Destruimos la instancia de la primera arma

            _equippedWeapons[0] = _equippedWeapons[1];
            OnWeaponEquipped?.Invoke(); // Creamos nueva instancia de la segunda arma

            _equippedWeapons[1] = aux;

            OnInventoryChanged?.Invoke(); // Actualizamos la UI
        }
    }

    public bool IsInventoryFull()
    {
        return _inventoryItems.Count == InventoryMaxSize;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //int initialInventorySize = _inventoryItems.Count;

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
        //if (initialInventorySize != _inventoryItems.Count)
        //{
        //    Destroy(collision.gameObject);
        //}
    }
}
