using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private EntitySO _entityData;
    [SerializeField] private Image _healthBarValue; // Referencia al componente Image del GameObject que contiene la barra de vida

    private float _health;

    public float Health { get => _health; } // Solo lectura para mostrar al jugador
    public bool IsDead { get; private set; }

    private void Start()
    {
        SetHealth();
    }

    public void SetHealth()
    {
        _health = _entityData.Health;
        IsDead = false;
        UpdateHealthBar(_health, _entityData.Health);
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(ReceiveDamageBehaviour()); // Efecto visual de recibir daño
        
        _health -= damage;
        UpdateHealthBar(_health, _entityData.Health); // Actualizamos la barra de vida

        IsDead = _health <= 0; // Comprobamos si ha muerto con el ataque
    }

    private void UpdateHealthBar(float current, float max)
    {
        _healthBarValue.fillAmount = current / max;
    }

    private IEnumerator ReceiveDamageBehaviour()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(1f, 0.5f, 0.5f, 1f); // Tono rojo claro

        yield return new WaitForSeconds(0.5f);

        sprite.color = Color.white;
    }
}
