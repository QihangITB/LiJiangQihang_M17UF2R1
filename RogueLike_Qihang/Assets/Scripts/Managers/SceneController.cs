using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private const string PlayerTag = "Player", PauseMenu = "Pause";
    public static SceneController Instance { get; private set; }
    private PlayerManager _playerManager;

    private void Start()
    {
        Instance = this;

        GameObject playerObject = GameObject.FindGameObjectWithTag(PlayerTag);
        _playerManager = playerObject != null ? playerObject.GetComponent<PlayerManager>() : null;
    }

    /// <summary>
    /// Carga la escena del menú de pausa en modo aditivo si no está ya cargada.
    /// Si ya está cargada, la descarga.
    /// También bloquea la capacidad del jugador de interactuar durante la pausa.
    /// </summary>
    public void LoadPause()
    {
        if (!IsSceneAlreadyLoaded(PauseMenu))
        {
            LoadAdditiveSceneByName(PauseMenu);
            _playerManager.BlockPlayer = true;
        }
        else
        {
            UnloadPause();
        }
    }

    /// <summary>
    /// Quita la escena del menú de pausa que si o si estara cargada. 
    /// También desbloquea la capacidad del jugador de interactuar.
    /// </summary>
    public void UnloadPause()
    {
        SceneManager.UnloadSceneAsync(PauseMenu);
        _playerManager.BlockPlayer = false;
    }

    /// <summary>
    /// Comprueba si una escena específica está actualmente cargada.
    /// </summary>
    /// <param name="sceneName">El nombre de la escena que se desea comprobar.</param>
    /// <returns> true si la escena está cargada; false en caso contrario.</returns>
    private bool IsSceneAlreadyLoaded(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        return scene.isLoaded;
    }

    /// <summary>
    /// Carga una escena aditiva específica por su nombre.
    /// </summary>
    /// <param name="sceneName">Nombre de la escena aditiva.</param>
    public void LoadAdditiveSceneByName(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }

    /// <summary>
    /// Carga una escena específica por su nombre.
    /// </summary>
    /// <param name="sceneName">Nombre de la escena a cargar.</param>
    public void LoadSceneByName(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    /// <summary>
    /// Sale del juego. Funciona solo en builds (no en el editor).
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
