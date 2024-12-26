using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    private const string WeaponTag = "Weapon", ConsumableTag = "Consumable";
    private const int InventoryMaxSize = 6, WeaponSlotMaxSize = 2, ConsumableSlotMaxSize = 3;

    public static InventoryManager Instance;

    private List<ItemSO> _inventoryItems = new List<ItemSO>();
    private WeaponSO[] _equippedWeapons = new WeaponSO[WeaponSlotMaxSize];
    private ConsumableSO[] _equippedConsumables = new ConsumableSO[ConsumableSlotMaxSize];

    public List<ItemSO> Items{ get => _inventoryItems; }

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

        // Solo se equipa el arma que es distinto al que esta equipado
        _equippedWeapons[slotNumber] = slotWeapon.Id != weapon.Id ? weapon : slotWeapon;
    }

    public void Equip(GameObject slot, ConsumableSO consumable)
    {
        int slotNumber = slot.transform.GetSiblingIndex();
        ConsumableSO slotConsumable = _equippedConsumables[slotNumber];

        // Solo se equipa el consumible que es distinto al que esta equipado
        _equippedConsumables[slotNumber] = slotConsumable.Id != consumable.Id ? consumable : slotConsumable;
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
