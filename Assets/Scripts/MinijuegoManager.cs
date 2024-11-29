using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MinijuegoManager : MonoBehaviour
{
    public static bool gameStarted = false;

    private void Start()
    {
        gameStarted = false;  // Asegurarse de que el juego no haya comenzado al inicio
    }

    private void Update()
    {
        if (!GameManager.gameStarted && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
        {
            GameManager.gameStarted = true;
            Debug.Log("¡El minijuego ha comenzado! (instrucción cambiada en MinijuegoManager)");
        }
    }

    // Método que se llama cuando termina el minijuego
    public void EndMinigame()
    {
        if (PlayerManager.Instance != null)
        {
            Debug.Log($"Terminando el minijuego. Turno actual: Jugador {PlayerManager.Instance.currentPlayer}");
            PlayerManager.Instance.NextTurn(); // Cambiar de turno
        }

        StartCoroutine(LoadSceneWithDelay("Jugador", 0.5f)); // Volver a la pantalla intermedia
    }


    // Corutina que carga la escena después de un retraso
    private IEnumerator LoadSceneWithDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);  // Retrasar la carga de la escena
        SceneManager.LoadScene(sceneName);  // Cargar la escena
    }

    public void LoadNextMinigame()
    {
        int nextMinigame = PlayerManager.Instance.currentMinigame + 1;
        string sceneName = "Minigame" + nextMinigame;

        if (nextMinigame > PlayerManager.Instance.totalMinigames)
        {
            SceneManager.LoadScene("End");
        }
        else if (SceneExists(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("La escena " + sceneName + " no existe.");
        }
    }

    private bool SceneExists(string sceneName)
    {
        // Verifica si la escena existe
        return Application.CanStreamedLevelBeLoaded(sceneName);
    }

}
