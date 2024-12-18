using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    void OnParticleCollision(GameObject collision)
    {
        Debug.Log("Hit: " + collision.name);
    }
}
