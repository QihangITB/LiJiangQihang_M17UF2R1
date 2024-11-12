using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEntity : MonoBehaviour
{
    public float health;
    public float value;

    public void Death() { }

    public void Attack() { }

}
