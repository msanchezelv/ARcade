using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameStarted = false;
    private bool sceneLoaded = false;

    public static GameManager Instance { get; private set; }

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);  // No destruir este objeto entre escenas
    //    }
    //    else
    //    {
    //        Destroy(gameObject);  // Si ya existe, destruir este objeto
    //    }
    //}

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void Update()
    {
        if (!gameStarted && (Input.touchCount > 0 || Input.GetMouseButtonDown(0))) // Detecta el toque o clic
        {
            gameStarted = true;
            Debug.Log("¡El minijuego ha comenzado!");
        }
    }

    // Método para cerrar la aplicación
    public void QuitGame()
    {
        Time.timeScale = 0; // Pausa el juego
        Application.Quit(); // Cierra la aplicación

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }


    // Prueba para comprobar que salta al minijuego que toca
    //public void LoadNextMinigame()
    //{
    //    int nextMinigame = PlayerManager.Instance.currentMinigame + 1;

    //    if (nextMinigame > PlayerManager.Instance.totalMinigames)
    //    {
    //        SceneManager.LoadScene("End");
    //    }
    //    else
    //    {
    //        string sceneName = "Minigame" + nextMinigame;
    //        SceneManager.LoadScene(sceneName);
    //    }
    //}
}
