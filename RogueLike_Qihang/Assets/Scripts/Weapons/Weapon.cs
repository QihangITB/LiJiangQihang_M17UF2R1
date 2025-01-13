using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, ICollectable
{
    public const string PlayerTag = "Player";

    public WeaponSO WeaponData;
    public GameObject WeaponPrefab;

    public void UseWeapon(GameObject dynamicPrefab)
    {
        // Pasamos el prefab dinamico en lugar del estatico
        WeaponData.UseWeapon(dynamicPrefab);
    }

    public void Collect(GameObject target)
    {
        CoinManager coinManager = target.GetComponent<CoinManager>();
        coinManager.RemoveCoins(WeaponData.Cost);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PlayerTag))
        {
            if (!InventoryManager.Instance.IsInventoryFull())
            {
                Collect(other.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
