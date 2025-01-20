using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveCoinToPlayer : MonoBehaviour
{
    private const string PlayerTag = "Player";
    private const float Empty = 0;

    [SerializeField] private float _coins;
    private CircleCollider2D _interactionRange;
    private CoinManager _playerCoin;

    private void Start()
    {
        _interactionRange = GetComponent<CircleCollider2D>();
        _playerCoin = GameObject.Find(PlayerTag).GetComponent<CoinManager>();
    }

    private void Update()
    {
        if(GiveCondition())
        {
            _playerCoin.AddCoins(_coins);
        }
    }

    private bool GiveCondition()
    {
        // La condicion es haber hablado con el vendedor y tener 0 de monedas
        return _interactionRange.enabled == false && _playerCoin.Coins == Empty;
    }

}
