using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    private readonly Color LightGreen = new Color(0f, 1f, 0f, 0.4f);
    private readonly Color DefaultColor = new Color(1f, 1f, 1f, 1f);

    public GameObject Item;                 // Incluye todo los objetos (armas y consumibles)
    public GameObject EquippedWeapon;       // Incluye solo las armas equipadas
    public GameObject EquippedConsumable;   // Incluye solo los consumibles equipados

    private List<GameObject> _itemSlots;
    private List<GameObject> _weaponSlots;
    private List<GameObject> _consumableSlots;
    private InventoryManager _inventoryManager;

    [NonSerialized] public ItemSO selectedItem;
    private bool _isInventoryActive = false;

    private void Start()
    {
        _isInventoryActive = true;
    }

    private void OnEnable()
    {
        _inventoryManager = InventoryManager.Instance;
        InitializeAllSlots();
        if(_isInventoryActive)
            UpdateSlotUI();
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

    private void UpdateSlotUI()
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
            if (i < inventory.Count)
            {
                if (inventory[i] != null)
                    SetSlotImage(slots[i], inventory[i].Icon);
            }
            else
            {
                // Desactiva la imagen del slot si no hay item
                GetSlotImage(slots[i]).enabled = false;
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

            if (selectedItem is WeaponSO weapon)
            {
                _inventoryManager.Equip(_weaponSlots[slotNumber], weapon);
            }
            else if (selectedItem is ConsumableSO consumable)
            {
                _inventoryManager.Equip(_consumableSlots[slotNumber], consumable);
            }

            UpdateSlotUI();
        }
    }   

    public void SelectItem()
    {
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
        yield return new WaitForSeconds(2.5f);

        // Vuelve a cambiar el color de los slots de armas a blanco
        ModifySlotUI(slots, DefaultColor);
    }

    private void ModifySlotUI(List<GameObject> slots, Color indicationColor)
    {
        foreach (GameObject slot in slots)
        {
            Image slotImage = slot.GetComponent<Image>();
            slotImage.color = indicationColor;
        }
    }

    private void ModifySlotUI(List<GameObject> firstSlots, List<GameObject> secondSlots, Color indicationColor)
    {
        ModifySlotUI(firstSlots, indicationColor);
        ModifySlotUI(secondSlots, indicationColor);
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
