using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;   // Referencia al TextMeshPro que muestra el tiempo
    public float gameTime = 30f;       // Duración del juego en segundos
    private float timeRemaining;       // Tiempo restante del juego
    private bool gameStarted = false;
    private PlayerManager playerManager;

    void Start()
    {
        timeRemaining = gameTime;  // Iniciar el temporizador con el valor máximo
        UpdateTimeText();          // Actualiza el texto del tiempo al inicio
        playerManager = PlayerManager.Instance;
    }

    void Update()
    {
        if (!gameStarted && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
        {
            gameStarted = true;
        }

        if (gameStarted)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimeText();

            if (timeRemaining <= 0f) //|| FindObjectOfType<LivesManager>().lives <= 0)
            {
                EndGame();
            }
        }
    }

    void EndGame()
    {        
        if (playerManager != null)
        {
            Debug.Log("Fin del juego, cambiando turno...");
            playerManager.NextTurn(); // Cambiar turno si playerManager está asignado
            SceneManager.LoadScene("Jugador");
        }
        else
        {
            Debug.LogError("playerManager es nulo. No se puede cambiar de turno.");
        }
    }




    void UpdateTimeText()
    {
        timeText.text = Mathf.Ceil(timeRemaining).ToString(); // Mostrar el tiempo restante en el texto
    }
}
