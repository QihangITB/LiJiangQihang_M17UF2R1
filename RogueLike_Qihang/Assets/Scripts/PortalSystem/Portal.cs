using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private const string PlayerTag = "Player", GameScene = "Game";
    private const float waitTime = 0.5f;
    // Tono rojizo
    private Color CloseColor = new Color(214f / 255f, 74f / 255f, 74f / 255f, 1f);
    // Tono blanco
    private Color OpenColor = new Color(1, 1, 1, 1);

    private CircleCollider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _spriteRenderer.color = CloseColor;
        _collider.isTrigger = false;
        PortalKey.OnKeyCollected += OpenPortal;
    }

    private void OnDestroy()
    {
        PortalKey.OnKeyCollected -= OpenPortal;
    }

    private void OpenPortal()
    {
        _spriteRenderer.color = OpenColor;
        _collider.isTrigger = true;
    }

    private IEnumerator ChargeNextLevel()
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);

        SceneController.Instance.LoadSceneByName(GameScene);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerTag))
        {
            StartCoroutine(ChargeNextLevel());
        }
    }
}
