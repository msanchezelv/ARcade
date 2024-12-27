using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class VRTimeManager : MonoBehaviour
{
    public static VRTimeManager Instance { get; private set; }
    public TextMeshProUGUI timeText;   // TextMeshPro que muestra el tiempo
    public float totalGameTime = 100f; // Tiempo total del juego (100 segundos)
    public float timePerMouse = 10f;   // Tiempo para encontrar y destruir cada ratón
    public float timeRemaining;       // Tiempo restante para cada ratón
    public float gameTimeRemaining;   // Tiempo restante total del juego
    public bool gameStarted = false;  // Para verificar que el temporizador está activado
    public int miceDestroyed = 0;     // Contador de ratones destruidos
    private PlayerManager playerManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        timeRemaining = timePerMouse;
        gameTimeRemaining = totalGameTime;
        UpdateTimeText();
        playerManager = PlayerManager.Instance;
    }

    void Update()
    {
        if (gameStarted)
        {
            gameTimeRemaining -= Time.deltaTime;
            timeRemaining -= Time.deltaTime;
            UpdateTimeText();

            if (timeRemaining <= 0f)
            {
                VRLivesManager.Instance.DecreaseLife();
                Debug.Log("No se ha encontrado al ratón, generando nuevo ratón.");
                timeRemaining = timePerMouse;
            }

            if (gameTimeRemaining <= 0f)
            {
                EndGame("Tiempo agotado");
            }
        }
    }

    // Función para iniciar el temporizador desde el VRGameManager
    public void StartTimer()
    {
        gameStarted = true;
        Debug.Log("VRTimeManager: Temporizador iniciado.");
    }

    // Función que se llama cuando un ratón es destruido
    public void MouseDestroyed()
    {
        miceDestroyed++;
        timeRemaining = timePerMouse; // Reinicia el tiempo para el siguiente ratón

        if (miceDestroyed >= 10)
        {
            EndGame("¡Ganaste!");
        }
    }

    // Fin del juego
    public void EndGame(string reason)
    {
        Debug.Log($"Fin del juego: {reason}");
        if (playerManager != null)
        {
            playerManager.NextTurn();
            SceneManager.LoadScene("Jugador");
        }
        else
        {
            Debug.LogError("PlayerManager es nulo. No se puede cambiar de turno.");
        }
    }

    // Actualiza el texto en pantalla con el tiempo restante
    void UpdateTimeText()
    {
        timeText.text = $"{Mathf.Ceil(gameTimeRemaining)}";
    }
}
