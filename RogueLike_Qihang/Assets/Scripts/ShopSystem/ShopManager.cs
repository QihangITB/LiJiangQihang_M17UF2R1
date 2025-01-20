using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private const int MaxSize = 3;

    [SerializeField] private List<GameObject> _instancePlaces;

    private InventoryItems Items;
    private List<GameObject> _saleItems;

    public List<GameObject> SaleItems { get => _saleItems; }

    private void Start()
    {
        Items = GameManager.Instance.InventoryItems;
        _saleItems = new List<GameObject>();

        InitializeShop();
        CreateItemInstances(_instancePlaces, _saleItems);
    }

    private void InitializeShop()
    {
        while (_saleItems.Count < MaxSize)
        {
            GameObject item = GetRandomItems(Items.AllItems);

            if (!IsTheItemOnSell(_saleItems, item))
            {
                SetItemToSell(item);
            }
        }
    }

    private GameObject GetRandomItems(List<GameObject> items)
    {
        int randomIndex = UnityEngine.Random.Range(0, items.Count);
        return items[randomIndex];
    }

    private bool IsTheItemOnSell(List<GameObject> items, GameObject item)
    {
        return items.Contains(item);
    }

    private void SetItemToSell(GameObject item)
    {
        _saleItems.Add(item);
    }

    private void CreateItemInstances(List<GameObject> places, List<GameObject> items)
    {
        for (int i = 0; i < places.Count; i++)
        {
            Instantiate(items[i], places[i].transform.position, Quaternion.identity, places[i].transform);
        }
    }
}
