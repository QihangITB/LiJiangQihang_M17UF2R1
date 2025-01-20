using System.Collections;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    private const float delayTime = 1f;

    private NavMeshSurface navMeshSurface;

    private void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
        StartCoroutine(DelayedBaking(delayTime));
    }

    private IEnumerator DelayedBaking(float time)
    {
        yield return new WaitForSeconds(time);
        BakeNavMesh();
    }

    private void BakeNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }
}
