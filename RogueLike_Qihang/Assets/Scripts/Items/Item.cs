using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private const string PlayerTag = "Player";

    private PolygonCollider2D _collider;
    private CoinManager _targetCoinManager;
    protected virtual ItemSO ItemData { get; } // Los hijos proporcionan el ItemData específico

    private void Start()
    {
        _collider = GetComponent<PolygonCollider2D>();
    }

    private void Collect(CoinManager player, ItemSO item)
    {
        player.RemoveCoins(item.Cost);
    }

    private bool CanPlayerBuyIt(CoinManager player, ItemSO item)
    {
        return player.Coins >= item.Cost;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(PlayerTag))
        {
            _targetCoinManager = collision.gameObject.GetComponent<CoinManager>();
            _collider.isTrigger = CanPlayerBuyIt(_targetCoinManager, ItemData);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerTag))
        {
            if (_targetCoinManager == null)
                _targetCoinManager = collision.GetComponent<CoinManager>();

            if (!InventoryManager.Instance.IsInventoryFull())
            {
                Collect(_targetCoinManager, ItemData);
                Destroy(this.gameObject);
            }
        }
    }
}
