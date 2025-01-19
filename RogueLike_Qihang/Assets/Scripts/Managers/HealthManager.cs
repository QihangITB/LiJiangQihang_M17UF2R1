using UnityEngine.UI;
using UnityEngine;

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
        _health -= damage;
        UpdateHealthBar(_health, _entityData.Health); // Actualizamos la barra de vida

        IsDead = _health <= 0; // Comprobamos si ha muerto con el ataque
    }

    private void UpdateHealthBar(float current, float max)
    {
        _healthBarValue.fillAmount = current / max;
    }
}
