using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    [SerializeField] private string id; // Nos servira para identificar el item
    [SerializeField] private float cost;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;

    public string Id { get => id; }
    public float Cost { get => cost; }
    public string Description { get => description; }
    public Sprite Icon { get => icon; }
}
