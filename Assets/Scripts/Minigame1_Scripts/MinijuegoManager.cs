using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MinijuegoManager : MonoBehaviour
{
    public static bool gameStarted;

    private void Start()
    {
        gameStarted = false;
    }

    // Método que se llama cuando termina el minijuego
    public void EndMinigame()
    {
        
        PlayerManager.Instance?.NextTurn(); // Cambiar de turno

        StartCoroutine(LoadSceneWithDelay("Jugador", 10f)); // Se ha añadodo un retraso para mostrar la puntuación
    }

    // Corutina que carga la escena después de un retraso
    private IEnumerator LoadSceneWithDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    // NO BORRAR es para hacer pruebas y ver si pasa correctamente al siguiente minijuego
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
            Debug.LogError("MinijuegoManager: La escena " + sceneName + " no existe.");
        }
    }

    private bool SceneExists(string sceneName)
    {
        // Verifica si la escena existe
        return Application.CanStreamedLevelBeLoaded(sceneName);
    }

}
