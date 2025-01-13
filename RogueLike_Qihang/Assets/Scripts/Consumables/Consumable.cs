using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour, ICollectable
{
    public const string PlayerTag = "Player";

    public ConsumableSO ConsumableData;

    public void Collect(GameObject target)
    {
        CoinManager coinManager = target.GetComponent<CoinManager>();
        coinManager.RemoveCoins(ConsumableData.Cost);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PlayerTag))
        {
            if(!InventoryManager.Instance.IsInventoryFull())
            {
                Collect(other.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
