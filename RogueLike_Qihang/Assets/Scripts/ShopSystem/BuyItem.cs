using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyItem : MonoBehaviour
{
    private const string DollarSymbol = "$";

    [SerializeField] private TMP_Text _cost;

    private ShopManager _shopManager;
    private BoxCollider2D _collider;
    private GameObject _item;

    void Start()
    {
        _shopManager = transform.parent.GetComponent<ShopManager>();
        _shopManager.OnShopAreaEnter += CanPlayerBuyIt;
        _collider = GetComponent<BoxCollider2D>();
        StartCoroutine(DelayedInitialization());
    }

    private void OnDestroy()
    {
        if (_shopManager != null)
        {
            _shopManager.OnShopAreaEnter -= CanPlayerBuyIt;
        }
    }

    private IEnumerator DelayedInitialization()
    {
        yield return null; // Espera un frame
        int index = transform.GetSiblingIndex(); 
        _item = _shopManager.SaleItems[index];
        _cost.text = GetItemCost(_item).ToString() + DollarSymbol;
    }

    private void CanPlayerBuyIt(GameObject player)
    {
        float playerCoins = player.GetComponent<CoinManager>().Coins;
        float itemPrice = GetItemCost(_item);

        // Activamos el collider si el jugador NO puede comprar
        // Desactivamos el collider si el jugador SI puede comprar
        _collider.enabled = !(playerCoins >= itemPrice);
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
