using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public int currentPlayer = 1; // Jugador actual
    public int currentMinigame = 1; // Minijuego actual
    public int totalMinigames = 5; // Número total de minijuegos
    public GameManager gameManager;

    private void Awake()
    {
        Debug.Log($"Awake llamado en PlayerManager para {gameObject.name} en la escena {UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}");

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log($"Se encontró una instancia duplicada de PlayerManager en {gameObject.name}. Se destruirá.");
            Destroy(gameObject);
        }
    }


    //void Start()
    //{
    //    Debug.Log("Start llamado en PlayerManager");

    //    Verifica que gameManager esté asignado antes de usarlo
    //    if (gameManager == null)
    //    {
    //        Debug.LogError("El GameManager no ha sido asignado correctamente en el PlayerManager.");
    //        gameManager = FindObjectOfType<GameManager>();
    //    }

    //    Verificar si el IntermediateScreenManager está presente
    //    if (IntermediateScreenManager.Instance != null)
    //    {
    //        Debug.Log("Jugador actual al iniciar la escena: " + currentPlayer);
    //        Invoke("TryUpdatePlayerText", 0.5f);
    //        IntermediateScreenManager.Instance.UpdatePlayerText();
    //    }
    //    else
    //    {
    //        Debug.LogError("No se encontró el IntermediateScreenManager en la escena.");
    //    }

    //    if (IntermediateScreenManager.Instance == null)
    //    {
    //        GameObject intermediateScreen = new GameObject("IntermediateScreenManager");
    //        intermediateScreen.AddComponent<IntermediateScreenManager>();
    //    }
    //    else
    //    {
    //        Debug.Log("PLayerManager: Jugador actual al iniciar la escena: " + currentPlayer);
    //        Invoke("TryUpdatePlayerText", 0.5f);
    //        IntermediateScreenManager.Instance.UpdatePlayerText();

    //    }
    //}

    // Método que se llama al presionar el botón para comenzar el minijuego
    //public void StartMinigame()
    //{
    //    GameManager gameManager = FindObjectOfType<GameManager>();

    //    if (gameManager != null)
    //    {
    //        string sceneName = "Minigame" + currentMinigame;
    //        SceneManager.LoadScene(sceneName);
    //    }
    //    else
    //    {
    //        Debug.LogError("GameManager no está disponible para cargar el minijuego.");
    //    }
    //}

    public void NextTurn()
    {
        currentPlayer = currentPlayer == 1 ? 2 : 1; // Alternar entre jugador 1 y 2
        currentMinigame++;
        Debug.Log($"Turno cambiado: Jugador {currentPlayer}");


        if (currentMinigame > totalMinigames)
        {
            SceneManager.LoadScene("End"); // Fin del juego si se completaron todos los minijuegos
        }
        else
        {
            SceneManager.LoadScene("Minigame" + currentMinigame); // Cargar el próximo minijuego
        }
    }



    void TryUpdatePlayerText()
    {
        if (IntermediateScreenManager.Instance != null)
        {
            IntermediateScreenManager.Instance.UpdatePlayerText(currentPlayer);
            Debug.Log("Texto del jugador actualizado: Jugador " + currentPlayer);
        }
        else
        {
            Debug.LogWarning("IntermediateScreenManager no está disponible para actualizar el texto.");
        }
    }

}
