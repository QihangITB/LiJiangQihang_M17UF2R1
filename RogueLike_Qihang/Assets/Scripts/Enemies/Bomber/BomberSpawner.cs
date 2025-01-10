using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberSpawner : MonoBehaviour
{
    private const int minChild = 0, maxChild = 5;

    public GameObject prefab;
    public int bomberNumber = 10; // Por defecto el numero de enemgios bomba es 10
    public float spawnTime = 3f; // Por defecto aparecen cada 3 segundos
    public static Stack<GameObject> bombersPool = new Stack<GameObject>();

    private float _spawnTimer = 0f;
    private int _generationCount;
    private int _creationCount;

    private void Start()
    {
        bombersPool = new Stack<GameObject>();
        _generationCount = 0;
        _creationCount = 0;

        // Si no hay enemigos sificientes en el pool, se crean y se guardan
        for (int i = 0; i < bomberNumber; i++)
        {
            if (i > bombersPool.Count)
            {
                _creationCount++;
            }
        }
    }

    private void Update()
    {
        _spawnTimer += Time.deltaTime;

        // Se instancia enemigos cuando el temporizador llega al tiempo de aparición y no supere la cantidad
        if (_spawnTimer >= spawnTime && _generationCount < bomberNumber)
        {
            _spawnTimer = 0f;
            Transform spawnPoint = GetRandomSpawnPoint();
            GameObject bomber;

            if (_creationCount > 0)
            {
                bomber = CreateBomber(spawnPoint);
                _creationCount--;
            }
            else
            {
                bomber = PopBomber(spawnPoint);
            }

            bomber.transform.SetParent(spawnPoint);
            _generationCount++;
        }
    }

    private Transform GetRandomSpawnPoint()
    {
        int pointNumber = Random.Range(minChild, maxChild);
        return transform.GetChild(pointNumber);
    }

    public static void PushBomber(GameObject bomber)
    {
        bomber.SetActive(false);
        bombersPool.Push(bomber);
    }

    private GameObject CreateBomber(Transform spawnPoint)
    {
        return Object.Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }

    private GameObject PopBomber(Transform spawnPoint)
    {
        GameObject bomber = bombersPool.Pop();
        bomber.transform.position = spawnPoint.position;
        bomber.SetActive(true);
        return bomber;
    }
}
