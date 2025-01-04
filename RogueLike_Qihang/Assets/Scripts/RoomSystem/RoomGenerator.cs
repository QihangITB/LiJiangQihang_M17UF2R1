using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomGenerator : MonoBehaviour
{
    private const string GameManager = "GameManager";
    private const string TopSide = "T", BottomSide = "B", LeftSide = "L", RightSide = "R";

    private RoomTemplates _templates;

    private void Start()
    {
        _templates = GameObject.Find(GameManager).GetComponent<RoomTemplates>();

        string entranceSide = GetRoomEntranceSide();

        // Si quedan salas aleatorias por instanciar, se selecciona una aleatoria, sino se selecciona una cerrada
        GameObject room = RoomManager.RandomRoomCount > 0 ? GetRandomRoomBySide(entranceSide) : GetCloseRoomBySide(entranceSide);

        CreateRoomInstance(room);
    }

    private void CreateRoomInstance(GameObject room)
    {
        if(RoomManager.IsPositionAvailable(transform.position) && room != null)
        {
            Instantiate(room, transform.position, Quaternion.identity);

            // Decrementar el contador de salas aleatorias ya que se ha instanciado una
            RoomManager.RandomRoomCount--;
        }
        else
        {
            Debug.Log("Pasillo destruido y cambiado");
            Instantiate(_templates.CloseHallways, transform.parent.position, transform.parent.rotation);
            Destroy(transform.parent.gameObject);
        }
    }

    private GameObject GetRandomRoomBySide(string side)
    {
        // Selecciona las salas que contienen el lado especificado
        GameObject[] rooms = _templates.AllRooms.Where(go => go.name.Contains(side)).ToArray();

        return SelectRandomRoom(rooms);
    }

    private GameObject SelectRandomRoom(GameObject[] rooms)
    {
        // Devuelve una sala aleatoria de la lista o null si está vacía
        return rooms.Length > 0 ? rooms[Random.Range(0, rooms.Length)] : null;
    }


    private GameObject GetCloseRoomBySide(string side)
    {
        // Selecciona las salas cerradas que contienen el lado especificado
        return _templates.CloseRooms.Where(go => go.name.Contains(side)).FirstOrDefault();
    }

    private string GetRoomEntranceSide()
    {
        // Obtener la rotación Z del padre en ángulos de Euler y en valor absoluto
        float parentRotationZ = Mathf.Abs(transform.parent.rotation.eulerAngles.z);

        switch(parentRotationZ)
        {
            case 0:
                return BottomSide;
            case 90:
                return RightSide;
            case 180:
                return TopSide;
            case 270:
                return LeftSide;
            default:
                return string.Empty;
        }

        // Esta rotacion indica la ENTRADA no confundir con SALIDA, que es la inversa
    }
}
