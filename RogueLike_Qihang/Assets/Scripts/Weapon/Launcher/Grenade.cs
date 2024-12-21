using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private const string ParamExplote = "Explote";

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //TODO
        // Hacer daño al enemigo
        Debug.Log("Hit: " + collision.gameObject.name);
        _animator.SetTrigger(ParamExplote);
    }
}
