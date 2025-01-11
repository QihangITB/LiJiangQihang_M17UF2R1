using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int RoomGenerationCount = 6; // NO es el numero de salas que se generaran
    [NonSerialized] public int CurrentCount;
    [NonSerialized] public List<GameObject> Rooms = new List<GameObject>();

    private void Awake()
    {
        RestartCounter();
    }

    public void RestartCounter()
    {
        CurrentCount = RoomGenerationCount;
    }

    public static bool IsPositionAvailable(Vector3 position)
    {
        // Detectar si hay un collider en la posición actual
        Collider2D existingCollider = Physics2D.OverlapPoint(position);
        return existingCollider == null;
    }

    public void AddRoom(GameObject room)
    {
        Rooms.Add(room);
    }

    public void CleanRooms()
    {
        Rooms.Clear();
    }

    //AQUI SE GENERARA BOSS, TIENDA, SALA DE RECOMPENSAS, ETC

}
