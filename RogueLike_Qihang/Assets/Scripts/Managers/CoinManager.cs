using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private EntitySO _entityData;

    private float _coinsValue;

    public float Coins { get => _coinsValue; } // Solo lectura para mostrar al jugador
    public event Action<float> OnCoinsChanged;

    private void Start()
    {
        SetCoins();
    }

    public void SetCoins()
    {
        _coinsValue = _entityData.Coins;
    }

    public void AddCoins(float value)
    {
        _coinsValue += value;
        OnCoinsChanged?.Invoke(_coinsValue);
    }

    public void RemoveCoins(float value)
    {
        _coinsValue -= value;
        OnCoinsChanged?.Invoke(_coinsValue);
    }

    public void IncreasePlayerCoins(GameObject player)
    {
        // Aumentamos las monedas del jugador
        player.GetComponent<CoinManager>().AddCoins(_coinsValue);

        // Quitamos las monedas del enemigo porque se lo ha "dado" al jugador
        RemoveCoins(_coinsValue);
    }
}
