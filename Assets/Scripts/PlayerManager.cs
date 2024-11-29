using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public int currentPlayer = 1; // Jugador actual
    public int currentMinigame = 1; // Minijuego actual
    public int totalMinigames = 5; // N�mero total de minijuegos
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
            Debug.Log($"Se encontr� una instancia duplicada de PlayerManager en {gameObject.name}. Se destruir�.");
            Destroy(gameObject);
        }
    }


    //void Start()
    //{
    //    Debug.Log("Start llamado en PlayerManager");

    //    Verifica que gameManager est� asignado antes de usarlo
    //    if (gameManager == null)
    //    {
    //        Debug.LogError("El GameManager no ha sido asignado correctamente en el PlayerManager.");
    //        gameManager = FindObjectOfType<GameManager>();
    //    }

    //    Verificar si el IntermediateScreenManager est� presente
    //    if (IntermediateScreenManager.Instance != null)
    //    {
    //        Debug.Log("Jugador actual al iniciar la escena: " + currentPlayer);
    //        Invoke("TryUpdatePlayerText", 0.5f);
    //        IntermediateScreenManager.Instance.UpdatePlayerText();
    //    }
    //    else
    //    {
    //        Debug.LogError("No se encontr� el IntermediateScreenManager en la escena.");
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

    // M�todo que se llama al presionar el bot�n para comenzar el minijuego
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
    //        Debug.LogError("GameManager no est� disponible para cargar el minijuego.");
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
            SceneManager.LoadScene("Minigame" + currentMinigame); // Cargar el pr�ximo minijuego
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
            Debug.LogWarning("IntermediateScreenManager no est� disponible para actualizar el texto.");
        }
    }

}
