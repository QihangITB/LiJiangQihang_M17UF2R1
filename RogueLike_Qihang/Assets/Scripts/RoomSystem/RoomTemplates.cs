using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    [Header("Rooms")]
    [SerializeField] private List<GameObject> _allRooms;
    [SerializeField] private List<GameObject> _closeRooms;

    [Header("Rooms Hallways")]
    [SerializeField] private GameObject _hallways;
    [SerializeField] private GameObject _closeHallways;

    [Header("Special Rooms")]
    [SerializeField] private GameObject _portal;
    [SerializeField] private GameObject _key;
    [SerializeField] private GameObject _shop;

    public List<GameObject> AllRooms { get => _allRooms; }
    public List<GameObject> CloseRooms { get => _closeRooms; }

    public GameObject Hallways { get => _hallways; }
    public GameObject CloseHallways { get => _closeHallways; }

    public GameObject Portal { get => _portal; }
    public GameObject Key { get => _key; }
    public GameObject Shop { get => _shop; }
}
