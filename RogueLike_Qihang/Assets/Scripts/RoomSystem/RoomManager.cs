using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private const float SpecialRoomDelay = 1f;
    private const int Offset = 1, MinValue = 1;

    public int RoomGenerationCount = 6; // NO es el numero de salas que se generaran
    [NonSerialized] public int CurrentCount;
    [NonSerialized] public List<GameObject> Rooms = new List<GameObject>();
    [NonSerialized] public List<Transform> RoomsPosition = new List<Transform>();

    private RoomTemplates _templates;
    private int _keyRoomIndex;

    private void Awake()
    {
        RestartCounter();
    }

    private void Start()
    {
        _templates = GameManager.Instance.RoomTemplates;
        StartCoroutine(GenerateSpecialRooms());
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

    public void AddRoom(GameObject room, Transform currentPosition)
    {
        Rooms.Add(room);
        RoomsPosition.Add(currentPosition); // La posición de la sala generada NO la del prefab
    }

    public void CleanRooms()
    {
        Rooms.Clear();
    }

    private IEnumerator GenerateSpecialRooms()
    {
        yield return new WaitForSeconds(SpecialRoomDelay); // Esperar a que se generen todas las salas

        int minRoom = MinValue;
        int maxRoom = Rooms.Count;
        GeneratePortalRoom(maxRoom);
        GenerateKeyRoom(minRoom, maxRoom); // La llave se ha de generar antes que la tienda
        GenerateShopRoom(minRoom, maxRoom); // La tienda no puede estar en la misma sala que la llave
    }


    private void GeneratePortalRoom(int max)
    {
        PlaceObjectInRoom(_templates.Portal, 
                            Rooms[max - Offset], 
                            RoomsPosition[Rooms.Count - Offset],  
                            true);
     }

    private void GenerateKeyRoom(int min, int max)
    {
        _keyRoomIndex = UnityEngine.Random.Range(min, max);
        PlaceObjectInRoom(_templates.Key, 
                            Rooms[_keyRoomIndex], 
                            RoomsPosition[_keyRoomIndex]);
    }

    private void GenerateShopRoom(int min, int max)
    {
        int shopRoomIndex;
        do {
            shopRoomIndex = UnityEngine.Random.Range(min, max);
        } while (shopRoomIndex == _keyRoomIndex);

        PlaceObjectInRoom(_templates.Shop, 
                            Rooms[shopRoomIndex], 
                            RoomsPosition[shopRoomIndex], 
                            true);
    }

    private void PlaceObjectInRoom(GameObject template, GameObject room, Transform roomPosition, bool disableEnemies = false)
    {
        Instantiate(template, roomPosition.position, Quaternion.identity);

        if (disableEnemies)
            DisableRoomEnemies(room);
    }

    private void DisableRoomEnemies(GameObject room)
    {
        EdgeCollider2D accessDetection = room.GetComponentInChildren<EdgeCollider2D>();
        if (accessDetection != null)
        {
            accessDetection.enabled = false; // Desactivar el collider para que nunca se active la sala
        }
    }
}
