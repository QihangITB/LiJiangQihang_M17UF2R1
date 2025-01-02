using UnityEngine;
using UnityEngine.AI;

public class PatrolBehaviour : MonoBehaviour
{
    public float PatrolRange = 5f; // Rango de patrullaje, por defecto es 5
    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public bool HasArrivedToDestination()
    {
        return !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance;
    }

    public void SetRandomDestination()
    {
        Vector2 position = GetRandomNavigablePoint();
        if (position != Vector2.zero)
        {
            _agent.SetDestination(position);
        }
    }

    // Método principal que genera y valida el punto
    private Vector2 GetRandomNavigablePoint()
    {
        Vector2 newPosition = Vector2.zero;
        bool isValid = false;

        // Continuar generando puntos hasta encontrar uno válido
        while (!isValid)
        {
            Vector2 randomPosition = GenerateRandomPosition();

            if (IsPositionValid(randomPosition))
            {
                isValid = true;
                newPosition = randomPosition; 
            }
        }
        return newPosition;
    }

    // Método para generar un punto aleatorio en coordenadas locales
    private Vector2 GenerateRandomPosition()
    {
        float randomX = Random.Range(-PatrolRange, PatrolRange);
        float randomY = Random.Range(-PatrolRange, PatrolRange);

        Vector2 localRandomPosition = new Vector2(randomX, randomY);

        // Convertir las coordenadas locales a coordenadas globales
        return transform.TransformPoint(localRandomPosition);
    }

    // Método para validar si un punto está en una zona caminable del NavMesh
    private bool IsPositionValid(Vector2 position)
    {
        return NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas);
    }
}
