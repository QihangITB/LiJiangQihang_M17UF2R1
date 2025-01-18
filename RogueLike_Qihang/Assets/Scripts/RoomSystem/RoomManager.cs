using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    private const float SpecialRoomDelay = 1.5f;
    private const int Offset = 1, MinValue = 1;

    public int RoomGenerationCount = 6; // NO es el numero de salas que se generaran
    [NonSerialized] public int CurrentCount;
    [NonSerialized] public List<GameObject> Rooms = new List<GameObject>();

    private RoomTemplates _templates;
    private int _keyRoomIndex;

    private void Awake()
    {
        RestartCounter();
    }

    private void Start()
    {
        _templates = GameManager.Instance.RoomTemplates;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public static bool IsPositionAvailable(Vector3 position)
    {
        // Detectar si hay un collider en la posición actual
        Collider2D existingCollider = Physics2D.OverlapPoint(position);
        return existingCollider == null;
    }

    private void RestartCounter()
    {
        CurrentCount = RoomGenerationCount;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(GenerateSpecialRooms());
    }

    public void AddRoom(GameObject room)
    {
        Rooms.Add(room);
    }

    public void CleanRooms()
    {
        Rooms.Clear();
    }

    private IEnumerator GenerateSpecialRooms()
    {
        yield return new WaitForSeconds(SpecialRoomDelay); // Esperar a que se generen todas las salas

        if (Rooms.Count == 0) yield break; // No hay salas generadas

        int minRoom = MinValue;
        int maxRoom = Rooms.Count - Offset;
        GeneratePortalRoom(maxRoom);
        GenerateKeyRoom(minRoom, maxRoom); // La llave se ha de generar antes que la tienda
        GenerateShopRoom(minRoom, maxRoom); // La tienda no puede estar en la misma sala que la llave
    }


    private void GeneratePortalRoom(int max)
    {
        PlaceObjectInRoom(_templates.Portal, 
                            Rooms[max], 
                            false);
     }

    private void GenerateKeyRoom(int min, int max)
    {
        _keyRoomIndex = UnityEngine.Random.Range(min, max);
        PlaceObjectInRoom(_templates.Key, 
                            Rooms[_keyRoomIndex]);
    }

    private void GenerateShopRoom(int min, int max)
    {
        int shopRoomIndex;
        do {
            shopRoomIndex = UnityEngine.Random.Range(min, max);
        } while (shopRoomIndex == _keyRoomIndex || shopRoomIndex == max);

        PlaceObjectInRoom(_templates.Shop, 
                            Rooms[shopRoomIndex], 
                            false);
    }

    private void PlaceObjectInRoom(GameObject objectPrefab, GameObject room, bool hasEnemies = true)
    {
        Instantiate(objectPrefab, room.transform.position, Quaternion.identity);
        
        if(!hasEnemies) DisableEnemies(room);
    }

    private void DisableEnemies(GameObject room)
    {
        AccessControl control = room.GetComponentInChildren<AccessControl>();
        if (control != null)
        {
            control.ConfigureAcces(false);
        }
    }
}
