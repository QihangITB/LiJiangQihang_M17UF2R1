using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayGenerator : MonoBehaviour
{
    private const string TopSide = "Top", BottomSide = "Bottom", LeftSide = "Left", RightSide = "Right";

    private RoomManager _manager;
    private RoomTemplates _templates;

    private void Start()
    {
        InitializeComponents();

        // Si quedan salas aleatorias por instanciar, creamos pasillos normales, sino creamos pasillos cerrados
        CreateHallwayInstance(_manager.CurrentCount > 0 ? _templates.Hallways : _templates.CloseHallways);
    }

    private void InitializeComponents()
    {
        _manager = GameManager.Instance.RoomManager;
        _templates = GameManager.Instance.RoomTemplates;
    }

    private void CreateHallwayInstance(GameObject hallway)
    {
        if (RoomManager.IsPositionAvailable(transform.position))
        {
            // Si no hay pasillos en la posición, procedemos a crearlos
            Instantiate(hallway, transform.position, GetHallwayRotation());
        }
    }

    private Quaternion GetHallwayRotation()
    {
        // La rotacion (0, 0, 0) indica que el pasillo esta en la posición "Top"
        // ENTRADA: Top -> SALIDA: Bottom
        switch (this.name)
        {
            case TopSide:
                return Quaternion.Euler(0, 0, 0);
            case BottomSide:
                return Quaternion.Euler(0, 0, 180);
            case LeftSide:
                return Quaternion.Euler(0, 0, 90);
            case RightSide:
                return Quaternion.Euler(0, 0, 270);
            default:
                return Quaternion.identity;
        }
        // Esta rotacion indica la SALIDA no confundir con ENTRADA, que es la inversa
    }
}
