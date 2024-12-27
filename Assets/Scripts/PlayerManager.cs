using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public int currentPlayer = 1; // Jugador actual
    public int currentMinigame = 1; // Minijuego actual
    public int totalMinigames = 5; // N�mero total de minijuegos
    public bool isSecondPlayerTurn = false;
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


    void Start()
    {
        Debug.Log("Start llamado en PlayerManager");

        //Verifica que gameManager est� asignado antes de usarlo
        if (gameManager == null)
        {
            Debug.LogError("El GameManager no ha sido asignado correctamente en el PlayerManager.");
            //gameManager = FindObjectOfType<GameManager>();
        }

        //Verificar si el IntermediateScreenManager est� presente
        if (IntermediateScreenManager.Instance != null)
        {
            Debug.Log("Jugador actual al iniciar la escena: " + currentPlayer);
            Invoke("TryUpdatePlayerText", 0.5f);
            IntermediateScreenManager.Instance.UpdatePlayerText(currentPlayer);
        }
    }

    public void NextTurn()
    {
        if (!isSecondPlayerTurn)
        {
            Debug.Log("Player Manager: Primero se calculará la puntuación final");
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            
            if (scoreManager != null)
            {
                scoreManager.CalculateAndShowFinalScore(); // Calcular y mostrar la puntuación
            }

            // Cambiar al segundo jugador
            currentPlayer = 2;
            isSecondPlayerTurn = true;
            Debug.Log($"Turno cambiado: Jugador {currentPlayer} jugar� Minijuego {currentMinigame}");
            SceneManager.LoadScene("Jugador"); // Mostrar la pantalla del jugador
        }
        else
        {
            // Ambos jugadores han jugado, avanzar al siguiente minijuego
            currentPlayer = 1; // Volver al primer jugador para el siguiente minijuego
            currentMinigame++;
            isSecondPlayerTurn = false;

            if (currentMinigame > totalMinigames)
            {
                Debug.Log("Todos los minijuegos completados. Fin del juego.");
                SceneManager.LoadScene("End"); // Fin del juego
            }
            else
            {
                Debug.Log($"Avanzando al Minijuego {currentMinigame} para el Jugador {currentPlayer}");
                SceneManager.LoadScene("Jugador"); // Mostrar la pantalla del jugador
            }
        }
    }

    public void StartMinigame()
    {
        // M�todo llamado desde el bot�n en la escena "Jugador"
        SceneManager.LoadScene("Minigame" + currentMinigame);
    }



    public void ScheduleNextTurn()
    {
        Debug.Log("Cambio de turno programado en PlayerManager.");
        Invoke(nameof(NextTurn), 1f); // Programar el cambio de turno despu�s de un retraso
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
