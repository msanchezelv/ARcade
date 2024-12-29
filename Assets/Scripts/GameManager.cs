using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameStarted = false;
    private bool sceneLoaded = false;
    public static GameManager Instance { get; private set; }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void Update()
    {
        if (!gameStarted && (Input.touchCount > 0 || Input.GetMouseButtonDown(0))) // Detecta el toque o clic
        {    
            gameStarted = true;
            Debug.Log("¡El minijuego ha comenzado!");
        }
    }

    // Metodo para cerrar la aplicacion
    public void QuitGame()
    {
        Time.timeScale = 0; // Pausa el juego
        Application.Quit(); // Cierra la aplicacion

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
