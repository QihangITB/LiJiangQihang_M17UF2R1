using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberSpawner : MonoBehaviour
{
    private const float DefaultSize = 1.5f;
    private const int minChild = 0, maxChild = 5;

    public GameObject prefab;
    public int bomberNumber = 10; // Por defecto el numero de enemgios bomba es 10
    public float spawnTime = 3f; // Por defecto aparecen cada 3 segundos

    // Pool estático compartido entre todas las instancias de BomberSpawner
    public static Stack<GameObject> bombersPool = new Stack<GameObject>();

    private float _spawnTimer = 0f;
    private int _generationCount;
    private int _creationCount;

    private void Start()
    {
        _generationCount = 0;
        _creationCount = 0;

        // Si no hay enemigos suficientes en el pool, se crean y se guardan
        for (int i = 0; i < bomberNumber; i++)
        {
            if (i >= bombersPool.Count)
            {
                _creationCount++;
            }
        }

        Debug.Log("Bombers created: " + _creationCount);
        Debug.Log("Bombers in pool: " + bombersPool.Count);
    }

    //private void OnEnable()
    //{
    //    _generationCount = 0;
    //    _creationCount = 0;

    //    // Si no hay enemigos suficientes en el pool, se crean y se guardan
    //    for (int i = 0; i < bomberNumber; i++)
    //    {
    //        if (i > bombersPool.Count)
    //        {
    //            _creationCount++;
    //        }
    //    }

    //    Debug.Log("Bombers created: " + _creationCount);
    //    Debug.Log("Bombers in pool: " + bombersPool.Count);
    //}

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

        // Si se han creado todos los enemigos bomba y todos estan muertos, se desactivan los spawners
        if (_generationCount == bomberNumber && !IsSomeoneAlive())
        {
            DisableSpawners();
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
        return Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }

    private GameObject PopBomber(Transform spawnPoint)
    {
        GameObject bomber = bombersPool.Pop();
        bomber.transform.localScale = new Vector3(DefaultSize, DefaultSize, DefaultSize);
        bomber.transform.position = spawnPoint.position;
        bomber.SetActive(true);
        return bomber;
    }

    private void DisableSpawners()
    {
        // Se desactivan todos sus hijos, que son los spawners
        foreach (Transform spawner in transform)
        {
            spawner.gameObject.SetActive(false);
        }
    }

    private bool IsSomeoneAlive()
    {
        // Recorre todos los spawners
        foreach (Transform spawner in transform)
        {
            // En cada spawner recorre todos los enemigos que han sido instanciados ahi
            foreach (Transform enemy in spawner)
            {
                if (enemy.gameObject.activeSelf)
                {
                    return true;
                }
            }
        }
        return false;
    }

}
