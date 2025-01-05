using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumableSO : ItemSO
{
    [SerializeField] private float effectTime;
    public float EffectTime { get => effectTime; }

    public abstract void UseConsumable();

}
