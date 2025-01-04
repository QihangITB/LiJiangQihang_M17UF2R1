using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static int RandomRoomCount = 5;

    public static bool IsPositionAvailable(Vector3 position)
    {
        // Detectar si hay un collider en la posición actual
        Collider2D existingCollider = Physics2D.OverlapPoint(position);
        return existingCollider == null;
    }

}
