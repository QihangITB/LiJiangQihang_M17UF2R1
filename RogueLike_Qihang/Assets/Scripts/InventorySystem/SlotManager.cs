using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    private readonly Color LightGreen = new Color(0f, 1f, 0f, 0.4f);
    private readonly Color DefaultColor = new Color(1f, 1f, 1f, 1f);
    private readonly Color LightRed = new Color(1f, 0f, 0f, 0.4f);

    public GameObject Item;                 // Incluye todo los objetos (armas y consumibles)
    public GameObject EquippedWeapon;       // Incluye solo las armas equipadas
    public GameObject EquippedConsumable;   // Incluye solo los consumibles equipados

    private List<GameObject> _itemSlots;
    private List<GameObject> _weaponSlots;
    private List<GameObject> _consumableSlots;
    private InventoryManager _inventoryManager;

    [NonSerialized] public ItemSO selectedItem;

    private void OnEnable()
    {
        _inventoryManager = InventoryManager.Instance;
        if (_inventoryManager == null) return;
        _inventoryManager.OnInventoryChanged += UpdateSlotUI;
        InitializeAllSlots();
        UpdateSlotUI();
    }

    private void OnDisable()
    {
        if (_inventoryManager == null) return;
        _inventoryManager.OnInventoryChanged -= UpdateSlotUI;
        ResetData();
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

    public void UpdateSlotUI()
    {
        if (_itemSlots != null)
            SetItemsToSlots(_itemSlots, _inventoryManager.Items);

        if (_weaponSlots != null)
            SetItemsToSlots(_weaponSlots, _inventoryManager.Weapons);

        if (_consumableSlots != null)
            SetItemsToSlots(_consumableSlots, _inventoryManager.Consumables);
    }

    private void SetItemsToSlots<T>(List<GameObject> slots, List<T> inventory) where T : ItemSO
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Image slotImage = GetSlotImage(slots[i]);

            if (i < inventory.Count && inventory[i] != null)
            {
                SetSlotImage(slots[i], inventory[i].Icon);
                slotImage.enabled = true;
            }
            else
            {
                slotImage.enabled = false; // Desactiva la imagen si no hay item o fuera del rango del inventario
            }
        }
    }

    private void SetSlotImage(GameObject slot, Sprite itemSprite)
    {
        Image slotImage = GetSlotImage(slot);
        slotImage.sprite = itemSprite;

        // Activa la imagen del slot
        slotImage.enabled = true;
    }

    private Image GetSlotImage(GameObject slot)
    {
        // Coge un slot y accede al componente de imagen situado en el primer hijo
        return slot.transform.GetChild(0).GetComponent<Image>();
    }

    public void DeleteItem()
    {
        int slotNumber = GetSlotIndexByParent(EventSystem.current.currentSelectedGameObject);

        // Para evitar errores de fuera de rango
        if (slotNumber < _inventoryManager.Items.Count)
        {
            ItemSO slotItem = _inventoryManager.Items[slotNumber];
            _inventoryManager.RemoveItem(slotItem);

            // Volvemos a actualizar los slots segun los items de la lista
            SetItemsToSlots(_itemSlots, _inventoryManager.Items);
        }
    }

    public void EquipItem()
    {
        if (selectedItem != null)
        {
            int slotNumber = GetSlotIndexByParent();
            GameObject slot = GetSlotForItem(selectedItem, slotNumber);

            if (slot != null)
            {
                if (!IsItemEquipped(selectedItem))
                    EquipSelectedItem(selectedItem, slot);
                else
                    ModifySlotUI(slot, LightRed);

                UpdateSlotUI();
            }
        }
    }

    private void EquipSelectedItem(ItemSO item, GameObject slot)
    {
        Action<ItemSO, GameObject> equipAction = GetEquipActionForItem(item);
        equipAction?.Invoke(item, slot);
    }

    private Action<ItemSO, GameObject> GetEquipActionForItem(ItemSO item)
    {
        if (item is WeaponSO)
            return (i, s) => _inventoryManager.Equip(s, i as WeaponSO);

        if (item is ConsumableSO)
            return (i, s) => _inventoryManager.Equip(s, i as ConsumableSO);

        return null;
    }

    private GameObject GetSlotForItem(ItemSO item, int slotNumber)
    {
        switch (item)
        {
            case WeaponSO:
                return _weaponSlots[slotNumber];
            case ConsumableSO:
                return _consumableSlots[slotNumber];
            default:
                return null;
        }
    }

    private bool IsItemEquipped(ItemSO item)
    {
        switch (item)
        {
            case WeaponSO weapon:
                return _inventoryManager.Weapons.Contains(weapon);
            case ConsumableSO consumable:
                return _inventoryManager.Consumables.Contains(consumable);
            default:
                return false;
        }
    }

    public void SelectItem()
    {
        ResetData();

        int slotNumber = GetSlotIndexByParent();

        selectedItem = slotNumber < _inventoryManager.Items.Count ? _inventoryManager.Items[slotNumber] : null;
        
        if (selectedItem != null)
        {
            // Si teniamos un item seleccionado, esta funcion reinicia los colores al default
            ModifySlotUI(_weaponSlots, _consumableSlots, DefaultColor);

            StartCoroutine(EquipWeaponIndication(selectedItem is WeaponSO ? _weaponSlots : _consumableSlots));
        }
    }

    private IEnumerator EquipWeaponIndication(List<GameObject> slots)
    {
        // Cambia el color de los slots de armas a verde durante 3 segundos
        ModifySlotUI(slots, LightGreen);
        yield return new WaitForSeconds(5f);

        // Volvemos a reiniciar los datos seleccionados
        ResetData();
    }

    private void ModifySlotUI(GameObject slot, Color indicationColor)
    {
        Image slotImage = slot.GetComponent<Image>();
        slotImage.color = indicationColor;
    }

    private void ModifySlotUI(List<GameObject> slots, Color indicationColor)
    {
        foreach (GameObject slot in slots)
        {
            ModifySlotUI(slot, indicationColor);
        }
    }

    private void ModifySlotUI(List<GameObject> firstSlots, List<GameObject> secondSlots, Color indicationColor)
    {
        ModifySlotUI(firstSlots, indicationColor);
        ModifySlotUI(secondSlots, indicationColor);
    }

    private void ResetData()
    {
        StopAllCoroutines();
        ModifySlotUI(_weaponSlots, _consumableSlots, DefaultColor);
        selectedItem = null;
    }

    public static int GetSlotIndexByParent()
    {
        // Obtiene el GameObject que ha sido clickeado
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;

        // Devuelve el indice del slot segun la posicion de la jerarquia respecto el gameObject padre
        return clickedButton.transform.GetSiblingIndex();
    }

    public static int GetSlotIndexByParent(GameObject clickedButton)
    {
        // Obtiene el padre del GameObject que ha sido clickeado
        Transform buttonParent = clickedButton.transform.parent;

        // Devuelve el indice respecto el gameObject padre, del padre del game object clickeado (pasado por parametro)
        return buttonParent.GetSiblingIndex();
    }

}
