using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private const int MaxSize = 3;

    [SerializeField] private List<Transform> _instancePlaces;

    private InventoryItems Items;
    private List<GameObject> SaleItems;

    private void Start()
    {
        Items = GameManager.Instance.InventoryItems;
        SaleItems = new List<GameObject>();

        InitializeShop();
        CreateItemInstances(_instancePlaces, SaleItems);
    }

    private void InitializeShop()
    {
        while (SaleItems.Count < MaxSize)
        {
            GameObject item = GetRandomItems(Items.AllItems);

            if (!IsTheItemOnSell(SaleItems, item))
            {
                SetItemToSell(item);
            }
        }
    }

    private GameObject GetRandomItems(List<GameObject> items)
    {
        int randomIndex = Random.Range(0, items.Count);
        return items[randomIndex];
    }

    private bool IsTheItemOnSell(List<GameObject> items, GameObject item)
    {
        return items.Contains(item);
    }

    private void SetItemToSell(GameObject item)
    {
        SaleItems.Add(item);
    }

    private void CreateItemInstances(List<Transform> places, List<GameObject> items)
    {
        for (int i = 0; i < places.Count; i++)
        {
            Instantiate(items[i], places[i].position, Quaternion.identity, places[i]);
        }
    }
}
