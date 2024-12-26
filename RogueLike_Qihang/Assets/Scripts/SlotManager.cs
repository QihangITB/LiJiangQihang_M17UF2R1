using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public GameObject Item;                 // Incluye todo los objetos (armas y consumibles)
    public GameObject EquippedWeapon;       // Incluye solo las armas equipadas
    public GameObject EquippedConsumable;   // Incluye solo los consumibles equipados

    private List<GameObject> _itemSlots;
    private List<GameObject> _weaponSlots;
    private List<GameObject> _consumableSlots;
    private InventoryManager _inventoryManager;

    private void OnEnable()
    {
        _inventoryManager = InventoryManager.Instance;
        InitializeAllSlots();
        SetItemsToSlots();
    }

    private void InitializeAllSlots()
    {
        if (Item != null)
            _itemSlots = GetListOfSlots(Item);

        if (EquippedWeapon != null)
            _weaponSlots = GetListOfSlots(EquippedWeapon);

        if (EquippedConsumable != null)
            _consumableSlots = GetListOfSlots(EquippedConsumable);
    }

    private List<GameObject> GetListOfSlots(GameObject slotsGroup)
    {
        List<GameObject> slots = new List<GameObject>();

        // Añade los hijos de slotsGroup a la lista de slots
        foreach (Transform slot in slotsGroup.transform)
        {
            slots.Add(slot.gameObject);
        }
        return slots;
    }

    private void SetItemsToSlots()
    {
        for (int i = 0; i < _inventoryManager.Items.Count; i++)
        {
            SetSlotImage(_itemSlots[i], _inventoryManager.Items[i]);
        }
    }

    private void SetSlotImage(GameObject slot, ItemSO item)
    {
        Image slotImage = GetSlotImage(slot);
        slotImage.sprite = item.Icon;
        slotImage.enabled = true;
    }

    private Image GetSlotImage(GameObject slot)
    {
        // Coge un slot y accede al componente de imagen situado en el primer hijo
        return slot.transform.GetChild(0).GetComponent<Image>();
    }
}
