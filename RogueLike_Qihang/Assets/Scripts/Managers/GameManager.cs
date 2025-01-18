using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    public RoomManager RoomManager { get; set; }
    public RoomTemplates RoomTemplates { get; set; }
    public InventoryItems InventoryItems { get; set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        RoomManager = GetComponent<RoomManager>();
        RoomTemplates = GetComponent<RoomTemplates>();
        InventoryItems = GetComponent<InventoryItems>();
    }

}
