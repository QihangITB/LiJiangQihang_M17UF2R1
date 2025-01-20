using UnityEngine;

[CreateAssetMenu(fileName = "Entity", menuName = "Entity/Entity")]
public class EntitySO : ScriptableObject
{
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float coins;
    [SerializeField] private AudioClip deathSound;

    public float Health { get => health; }
    public float Speed { get => speed; }
    public float Coins { get => coins; }
    public AudioClip DeathSound { get => deathSound; }
}
