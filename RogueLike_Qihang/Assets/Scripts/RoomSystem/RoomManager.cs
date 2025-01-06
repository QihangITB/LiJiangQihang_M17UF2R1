using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int RoomGenerationCount = 6; // NO es el numero de salas que se generaran

    public static bool IsPositionAvailable(Vector3 position)
    {
        // Detectar si hay un collider en la posición actual
        Collider2D existingCollider = Physics2D.OverlapPoint(position);
        return existingCollider == null;
    }

    //AQUI SE GENERARA BOSS, TIENDA, SALA DE RECOMPENSAS, ETC

}
