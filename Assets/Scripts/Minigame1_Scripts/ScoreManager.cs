using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score = 0;              // Puntuación inicial
    private LivesManager livesManager;
    private bool gameEnded = false;    // Controlar si el minijuego ha terminado

    private void Start()
    {
        livesManager = FindObjectOfType<LivesManager>();
        if (livesManager == null)
        {
            Debug.LogError("ScoreManager: No se encontró el componente LivesManager en la escena.");
        }

        // Asegurarse de que el texto esté desactivado al inicio
        if (scoreText != null)
        {
            scoreText.enabled = false;
        }
        else
        {
            Debug.LogError("ScoreManager: No se ha asignado un TextMeshPro al campo scoreText.");
        }
    }

    // Método para calcular y mostrar la puntuación al final del minijuego
    public void CalculateAndShowFinalScore()
    {
        if (gameEnded) return; // Evitar cálculos múltiples

        gameEnded = true; // Marcar el fin del juego
        if (livesManager != null)
        {
            score = livesManager.lives * 10;
            UpdateScoreText(); // Actualizar el texto con la puntuación
            Debug.Log($"Puntuación calculada: {score} (basado en {livesManager.lives} vidas restantes)");
        }
        else
        {
            Debug.LogError("No se pudo calcular la puntuación porque el LivesManager es nulo.");
        }

        // Mostrar la puntuación en pantalla temporalmente
        StartCoroutine(DisplayScoreTemporarily(15f));
    }

    // Actualizar el texto con la puntuación actual
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Puntuación: {score}";
        }
    }

    // Mostrar la puntuación en pantalla por unos segundos y luego ocultarla
    private IEnumerator DisplayScoreTemporarily(float duration)
    {
        if (scoreText != null)
        {
            scoreText.enabled = true;
            yield return new WaitForSeconds(duration);
            scoreText.enabled = false;
        }
    }
}
