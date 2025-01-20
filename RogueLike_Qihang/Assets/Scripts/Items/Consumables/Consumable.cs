using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    public ConsumableSO ConsumableData;
    protected override ItemSO ItemData => ConsumableData;

    // TODO: Implementar el uso de consumibles
    // public void UseConsumable()
    // {

    // }
}
