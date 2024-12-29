using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class VRTimeManager : MonoBehaviour
{
    public static VRTimeManager Instance { get; private set; }
    public TextMeshProUGUI timeText;   // Referencia al TextMeshPro que muestra el tiempo
    public float totalGameTime = 100f; // Tiempo total del juego (100 segundos)
    public float timePerMouse = 10f;   // Tiempo para encontrar y destruir cada ratón
    private float timeRemaining;       // Tiempo restante para cada ratón
    private float gameTimeRemaining;   // Tiempo restante total del juego
    private bool gameStarted = false;  // Para verificar que ha empezado el minijuego
    private int miceDestroyed = 0;     // Contador de ratones destruidos
    private PlayerManager playerManager;

    private void Start()
    {
        timeRemaining = timePerMouse;           // Iniciar el tiempo para cada ratón
        gameTimeRemaining = totalGameTime;      // Iniciar el tiempo total del juego
        UpdateTimeText();                       // Actualiza el texto del tiempo al inicio
        playerManager = PlayerManager.Instance;

    }

    private void Update()
    {
        
    }

    // Se destruyen ratones
    public void MouseDestroyed()
    {
        miceDestroyed++;
        timeRemaining = timePerMouse; // Reiniciamos el tiempo para el siguiente ratón

        if (miceDestroyed >= 10)
        {
            EndGame("¡Ganaste!");
        }
    }

    // Condiciones para terminar el minijuego
    public void EndGame(string reason)
    {        
        if (playerManager != null)
        {
            Debug.Log($"Fin del juego: {reason}");
            playerManager.NextTurn();
            SceneManager.LoadScene("Jugador");
        }
        else
        {
            Debug.LogError("playerManager es nulo. No se puede cambiar de turno.");
        }
    }

    // Actualiza el texto en pantalla con el tiempo restante
    private void UpdateTimeText()
    {
        timeText.text = $"{Mathf.Ceil(gameTimeRemaining)}";
    }

    internal void StartTimer()
    {
        gameTimeRemaining -= Time.deltaTime;    // Resta el tiempo total del juego
        timeRemaining -= Time.deltaTime;        // Resta el tiempo para cada ratón
        UpdateTimeText();
        
        if (timeRemaining <= 0f)
        {
            VRLivesManager.Instance.DecreaseLife();
            Debug.Log("No se ha encontrado al ratón, generando nuevo ratón");
            timeRemaining = timePerMouse;
        }
        
        if (gameTimeRemaining <= 0f)
        {
            EndGame("Tiempo agotado");
        }
    }
}
