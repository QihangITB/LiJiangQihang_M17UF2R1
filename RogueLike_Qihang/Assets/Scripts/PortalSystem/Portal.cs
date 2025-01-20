using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private const string PlayerTag = "Player";
    private const string TutorialScene = "Tutorial", GameScene = "Game", BucleScene = "Bucle";
    private const float waitTime = 1f;
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

        string currentScene = SceneController.Instance.GetCurrentSceneName();
        string nextScene = SelectNextScene(currentScene);

        SceneController.Instance.LoadSceneByName(nextScene);
    }

    private string SelectNextScene(string currentScene)
    {
        // Se usa switch case para escalabilidad
        return currentScene switch
        {
            GameScene => BucleScene,
            BucleScene => BucleScene,
            TutorialScene => GameScene,
            _ => GameScene // Por defecto, se cargará la escena de juego.
        };
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerTag))
        {
            StartCoroutine(ChargeNextLevel());
        }
    }
}
