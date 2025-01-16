using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class ScrollEffect : MonoBehaviour
{
    private const float LimitPosition = 5f;

    public float parallaxSpeed;
    public Vector2 moveDirection = new Vector2(1, 0); // (1, 0) para movimiento a la derecha

    private float _startPosition;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        _startPosition = transform.position.x;
    }

    void Update()
    {
        MoveBackground(); // Mueve el fondo independientemente del movimiento del jugador
        CheckAndResetPosition();
    }

    private void MoveBackground()
    {
        // Desplaza el fondo en función de la dirección, velocidad y tiempo automaticamente
        transform.position += new Vector3(moveDirection.x, moveDirection.y, 0) * parallaxSpeed * Time.deltaTime;
    }

    private void CheckAndResetPosition()
    {
        // Verifica si el fondo ha alcanzado el límite
        if (transform.position.x >= _startPosition + LimitPosition || transform.position.x <= _startPosition - LimitPosition)
        {
            ResetBackgroundPosition();
        }
    }

    private void ResetBackgroundPosition()
    {
        // Resetea la posición del fondo en función de la posición de la cámara
        _startPosition = transform.position.x;
        transform.position = new Vector3(mainCamera.transform.position.x, transform.position.y, transform.position.z);
    }
}
