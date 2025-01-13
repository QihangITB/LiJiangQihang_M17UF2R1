using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlashWings", menuName = "Item/Consumable/FlashWings")]
public class FlashWings : ConsumableSO
{
    public override void UseConsumable()
    {
        // FALTA DESARROLLAR: Aumenta la velocidad de movimiento durante un tiempo determinado
        Debug.Log($"{Id} ha sido utilizado con el siguiente efecto: {Description}");
    }
}
