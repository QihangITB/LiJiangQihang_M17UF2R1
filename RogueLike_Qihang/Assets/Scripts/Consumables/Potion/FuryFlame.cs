using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FuryFlame", menuName = "Consumable/FuryFlame")]
public class FuryFlame : ConsumableSO
{
    public override void UseConsumable()
    {
        // FALTA DESARROLLAR: Aumenta el daño de las armas durante un tiempo determinado
        Debug.Log($"{Id} ha sido utilizado con el siguiente efecto: {Description}");
    }
}
