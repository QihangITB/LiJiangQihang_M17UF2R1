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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Persistimos este objeto entre escenas.
            InitializeComponents();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeComponents()
    {
        RoomManager = GetComponent<RoomManager>();
        RoomTemplates = GetComponent<RoomTemplates>();
        InventoryItems = GetComponent<InventoryItems>();
    }

}
