using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VitalEssence", menuName = "Consumable/VitalEssence")]
public class VitalEssence : ConsumableSO
{
    public override void Use()
    {
        // FALTA DESARROLLAR: Cura al jugador con los puntos especificados
        Debug.Log($"{Id} ha sido utilizado con el siguiente efecto: {Description}");
    }
}
