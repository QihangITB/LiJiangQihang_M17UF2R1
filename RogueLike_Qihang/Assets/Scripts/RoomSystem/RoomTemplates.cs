using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    [SerializeField] private List<GameObject> _allRooms;
    [SerializeField] private List<GameObject> _closeRooms;

    [SerializeField] private GameObject _hallways;
    [SerializeField] private GameObject _closeHallways;

    public List<GameObject> AllRooms { get => _allRooms; }
    public List<GameObject> CloseRooms { get => _closeRooms; }

    public GameObject Hallways { get => _hallways; }
    public GameObject CloseHallways { get => _closeHallways; }
}
