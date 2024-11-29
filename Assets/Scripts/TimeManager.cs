using UnityEngine;
using TMPro;

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
    }

    void Update()
    {
        // Si el jugador toca la pantalla y el juego aún no ha comenzado
        if (!gameStarted && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
        {
            gameStarted = true;  // Iniciar el juego
        }

        // Solo reducir el tiempo si el juego ha comenzado
        if (gameStarted)
        {
            timeRemaining -= Time.deltaTime;  // Reducir el tiempo en función del frame
            UpdateTimeText();                 // Actualizar el texto del tiempo en pantalla

            if (timeRemaining <= 0f || FindObjectOfType<LivesManager>().lives <= 0)
            {
                EndGame();  // Si el tiempo se acaba o las vidas llegan a 0, el juego termina
            }
        }
    }

    void UpdateTimeText()
    {
        timeText.text = Mathf.Ceil(timeRemaining).ToString(); // Mostrar el tiempo restante en el texto
    }

    void EndGame()
    {
        playerManager.NextTurn();
    }
}
