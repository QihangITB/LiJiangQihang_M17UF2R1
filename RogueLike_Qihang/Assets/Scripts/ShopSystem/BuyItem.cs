using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyItem : MonoBehaviour
{
    private const string DollarSymbol = "$";

    [SerializeField] private TMP_Text _cost;

    private GameObject _item;

    void Start()
    {
        ShopManager shop = transform.parent.GetComponent<ShopManager>();
        StartCoroutine(DelayedSetPriceToUI(shop));
    }

    private IEnumerator DelayedSetPriceToUI(ShopManager shop)
    {
        yield return null; // Espera un frame

        int index = transform.GetSiblingIndex();
        _item = shop.SaleItems[index];
        _cost.text = GetItemCost(_item).ToString() + DollarSymbol;
    }

    private float GetItemCost(GameObject item)
    {
        float cost = 0f;

        // Verificamos si tiene el componente Weapon
        if (item.TryGetComponent<Weapon>(out Weapon weapon))
        {
            cost = weapon.WeaponData.Cost;
        }
        // Si no tiene Weapon, verificamos si tiene Consumable
        else if (item.TryGetComponent<Consumable>(out Consumable consumable))
        {
            cost = consumable.ConsumableData.Cost;
        }
        return cost;
    }
}
