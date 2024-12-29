using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;   // Referencia al TextMeshPro que muestra el tiempo
    public float gameTime = 30f;       // Duraci�n del juego en segundos
    private float timeRemaining;       // Tiempo restante del juego
    private bool gameStarted = false;
    private PlayerManager playerManager;

    private void Start()
    {
        timeRemaining = gameTime;
        UpdateTimeText();
        playerManager = PlayerManager.Instance;
    }

    private void Update()
    {
        if (!gameStarted && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
        {
            gameStarted = true;
        }

        if (gameStarted)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimeText();

            if (timeRemaining <= 0f)
            {
                EndGame();
            }
        }
    }

    private void EndGame()
    {        
        if (playerManager != null)
        {
            Debug.Log("Fin del juego, cambiando turno...");
            playerManager.NextTurn(); // Cambiar turno si playerManager esta asignado
            SceneManager.LoadScene("Jugador");
        }
        else
        {
            Debug.LogError("playerManager es nulo. No se puede cambiar de turno.");
        }
    }

    private void UpdateTimeText()
    {
        timeText.text = Mathf.Ceil(timeRemaining).ToString(); // Mostrar el tiempo restante en el texto
    }
}
