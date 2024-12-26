using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private float cost;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;

    public string Id { get => id; }
    public float Cost { get => cost; }
    public string Description { get => description; }
    public Sprite Icon { get => icon; }

    public abstract void Use();
}
