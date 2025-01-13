using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowPlayerCoin : MonoBehaviour
{
    private const string PlayerTag = "Player";

    public GameObject player;

    private CoinManager _coinManager;
    private TMP_Text _text;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag(PlayerTag);

        _coinManager = player.GetComponent<CoinManager>();
        _text = GetComponent<TMP_Text>();

        _coinManager.OnCoinsChanged += UpdateCoinText;
        UpdateCoinText(_coinManager.Coins); // Inicializa el texto.
    }

    void OnDestroy()
    {
        _coinManager.OnCoinsChanged -= UpdateCoinText;
    }

    void UpdateCoinText(float coins)
    {
        _text.text = coins.ToString();
    }
}
