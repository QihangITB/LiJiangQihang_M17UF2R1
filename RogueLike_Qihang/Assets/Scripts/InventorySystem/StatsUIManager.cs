using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatsUIManager : MonoBehaviour
{
    private const string DefaultDescription = "Select an item to see the description.";

    // Datos del personaje 
    [SerializeField] private TMP_Text _playerHealth;
    [SerializeField] private TMP_Text _playerSpeed;

    // Datos generales como item
    [SerializeField] public GameObject itemDescription;
    [SerializeField] public GameObject itemCost;

    // Datos del arma
    [SerializeField] public GameObject weaponDamage;
    [SerializeField] public GameObject weaponSpeed;

    // Datos del consumible
    [SerializeField] public GameObject consumableEffectTime;

    private TMP_Text _description, _cost, _damage, _speed, _effectTime;
    private InventoryManager _inventory;

    private void Awake()
    {
        InitializeTextComponent();
    }

    private void OnEnable()
    {
        _inventory = InventoryManager.Instance;

        if (_description.text == DefaultDescription)
            ToggleItemFields(false);
    }

    private void InitializeTextComponent()
    {
        _description = itemDescription.GetComponent<TMP_Text>();
        _cost = itemCost.transform.GetChild(0).GetComponent<TMP_Text>();
        _damage = weaponDamage.transform.GetChild(0).GetComponent<TMP_Text>();
        _speed = weaponSpeed.transform.GetChild(0).GetComponent<TMP_Text>();
        _effectTime = consumableEffectTime.transform.GetChild(0).GetComponent<TMP_Text>();
    }

    public void UpdateItemFields()
    {
        int slotNumber = SlotManager.GetSlotIndexByParent();

        ItemSO item = slotNumber < _inventory. Items.Count ? _inventory.Items[slotNumber] : null;

        if (item == null)
        {
            ToggleItemFields(false);
            _description.text = DefaultDescription;
        }
        else
        {
            ToggleItemFields(true);
            _description.text = item.Description;
            _cost.text = item.Cost.ToString();

            if (item is WeaponSO weapon)
            {
                ToggleConsumableFields(false);
                UpdateWeaponFields(weapon);
            }
            else if (item is ConsumableSO consumable)
            {
                ToggleWeaponFields(false);
                UpdateConsumableFields(consumable);
            }
        }
    }

    private void UpdateWeaponFields(WeaponSO weapon)
    {
        _damage.text = weapon.Damage.ToString();
        _speed.text = weapon.Speed.ToString();
    }

    private void UpdateConsumableFields(ConsumableSO consumable)
    {
        _effectTime.text = consumable.EffectTime.ToString();
    }

    private void ToggleItemFields(bool conditional)
    {
        // La descripción es el unico campo que se muestra siempre.
        itemCost.SetActive(conditional);
        ToggleWeaponFields(conditional);
        ToggleConsumableFields(conditional);
    }

    private void ToggleWeaponFields(bool conditional)
    {
        weaponDamage.SetActive(conditional);
        weaponSpeed.SetActive(conditional);
    }

    private void ToggleConsumableFields(bool conditional)
    {
        consumableEffectTime.SetActive(conditional);
    }

}
