using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    private const float LineWidth = 0.1f; // 0 para que no se vea la l�nea

    // Guardamos unos valores por defecto de la trayectoria para tener una estructura hecha
    private readonly Vector2[] DefaultPoints = new Vector2[]
    {
        new Vector2(0f, 0f),        // Inicio primera parabola
        new Vector2(0.3f, 0.5f),
        new Vector2(0.8f, 0.85f),
        new Vector2(1.5f, 1f),      // Primer punto maximo
        new Vector2(2.2f, 0.85f),
        new Vector2(2.7f, 0.5f),
        new Vector2(3f, 0f),        // Fin primera parabola, inicio segunda parabola
        new Vector2(3.25f, 0.3f),
        new Vector2(3.6f, 0.55f),
        new Vector2(4f, 0.6f),      // Segundo punto maximo
        new Vector2(4.4f, 0.55f),
        new Vector2(4.75f, 0.3f),
        new Vector2(5f, 0f)         // Final segunda parabola
    };

    private LineRenderer _lineRenderer;
    private bool _isMoving = true;    // Controla si el objeto est� en movimiento
    private int currentPointIndex = 0; // �ndice del punto actual

    public bool IsMoving { get => _isMoving;  set => _isMoving = value; }
    public float speed = 2f;         // Velocidad de movimiento a lo largo de la l�nea
    public Vector2[] points; // Puntos de la trayectoria a partir de P(0,0)

    void Start()
    {
        InitializeLineRenderer();
        GenerateTrajectory();
        transform.position = points[0];
    }

    void Update()
    {
        if (_isMoving && points.Length > 1)
        {
            FollowTrajectory();
        }
    }

    private void InitializeLineRenderer()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = LineWidth;
        _lineRenderer.endWidth = LineWidth;
    }

    private void GenerateTrajectory()
    {
        points = points.Length <= 0 ? DefaultPoints : points; // Si points no tiene valor, le asignamos el por defecto

        _lineRenderer.positionCount = points.Length;
        Quaternion rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z);

        for (int i = 0; i < points.Length; i++)
        {
            // Rotar cada punto seg�n la rotaci�n del objeto y asignarle la posici�n actual
            points[i] = (Vector2)(rotation * points[i]) + (Vector2)transform.position;

            // Lo a�adimos al LineRenderer
            _lineRenderer.SetPosition(i, points[i]);
        }
    }

    private void FollowTrajectory()
    {
        Vector2 targetPosition = points[currentPointIndex];

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Si llega al punto, avanzar al siguiente
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            currentPointIndex++;
            if (currentPointIndex >= points.Length)
            {
                // Detener el movimiento si se alcanzan todos los puntos
                _isMoving = false;
            }
        }
    }
}