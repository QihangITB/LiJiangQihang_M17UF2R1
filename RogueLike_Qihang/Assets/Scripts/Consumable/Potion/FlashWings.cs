using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlashWings", menuName = "Consumable/FlashWings")]
public class FlashWings : ConsumableSO
{
    public override void Use()
    {
        // FALTA DESARROLLAR: Aumenta la velocidad de movimiento durante un tiempo determinado
        Debug.Log($"{Id} ha sido utilizado con el siguiente efecto: {Description}");
    }
}
