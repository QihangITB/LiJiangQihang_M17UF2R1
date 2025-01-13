using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private const string PlayerTag = "Player";
    private const string NextButton = "Next", ShowButton = "Show", FinishButton = "Finish";
    private const float TypingTime = 0.05f;
    private const int Offset = 1;

    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private TMP_Text _dialogueText;
    [SerializeField] private DialogueSO _dialogue;
    [SerializeField] private GameObject[] _allSceneCanvas;

    private CircleCollider2D _dialogueRange;
    private List<GameObject> _disabledCanvas;
    private GameObject _exclamationMark;
    private PlayerManager _playerManager;
    private int _currentLine;
    private bool _isLineComplete;

    private void Start()
    {
        _dialogueRange = GetComponent<CircleCollider2D>();
        _disabledCanvas = new List<GameObject>();
        _exclamationMark = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if(_panel.activeSelf)
        {
            // Determinar si el texto actual coincide con la línea actual
            _isLineComplete = _dialogueText.text == _dialogue.Lines[_currentLine];

            // Configurar el texto del botón según el estado
            _buttonText.text = _isLineComplete ?
                                (_currentLine < _dialogue.Lines.Length - Offset ? NextButton : FinishButton) :
                                ShowButton;
        }
    }

    private void StartDialogue()
    {
        _currentLine = 0;                   // Iniciar desde la primera linea

        DisableUICanvas(_allSceneCanvas);   // Desactivar todos los canvas de la escena que esten activas

        _exclamationMark.SetActive(false);  // Desactivar el signo de exclamación para siempre
        _panel.SetActive(true);

        _playerManager.BlockPlayer = true;  // Bloqueamos al jugador
        Time.timeScale = 0;                 // Bloqueamos el entorno

        StartCoroutine(ShowLineByLetter()); // Mostrar el texto letra por letra
    }

    private void FinishDialogue()
    {
        _panel.SetActive(false);
        _dialogueRange.enabled = false;     // Desactivar el collider para que no se pueda volver a iniciar el dialogo

        EnableUICanvas(_allSceneCanvas);    // Activamos los canvas activos que desactivamos al iniciar el dialogo

        _playerManager.BlockPlayer = false; // Desbloqueamos al jugador
        Time.timeScale = 1f;                // Desbloqueamos el entorno
    }

    private void DisableUICanvas(GameObject[] canvas)
    {
        foreach (GameObject canva in canvas)
        {
            if (canva.activeSelf)
            {
                canva.SetActive(false);
                _disabledCanvas.Add(canva);
            }
        }
    }

    private void EnableUICanvas(GameObject[] canvas)
    {
        foreach (GameObject canva in canvas)
        {
            if (_disabledCanvas.Contains(canva))
            {
                canva.SetActive(true);
                _disabledCanvas.Remove(canva);
            }
        }
    }

    private IEnumerator ShowLineByLetter()
    {
        _dialogueText.text = string.Empty;

        foreach (char letter in _dialogue.Lines[_currentLine])
        {
            _dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(TypingTime);
        }

    }

    private void NextLine()
    {
        _currentLine++;
        if (_currentLine < _dialogue.Lines.Length)
        {
            StartCoroutine(ShowLineByLetter());
        }
        else
        {
            FinishDialogue();
        }
    }

    public void OnClickButton()
    {
        if(_isLineComplete)
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            _dialogueText.text = _dialogue.Lines[_currentLine];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(PlayerTag))
        {
            // Inicializamos el PlayerManager cuando entra al trigger
            _playerManager = collision.GetComponent<PlayerManager>();
            StartDialogue();
        }
    }
}
