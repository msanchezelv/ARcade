using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;   // Campo para asignar el TextMeshPro desde Unity
    public int score = 0;              // Puntuación inicial
    private LivesManager livesManager; // Referencia al LivesManager
    private bool gameEnded = false;    // Controlar si el minijuego terminó

    void Start()
    {
        livesManager = FindObjectOfType<LivesManager>(); // Obtener la referencia al LivesManager
        if (livesManager == null)
        {
            Debug.LogError("ScoreManager: No se encontró el componente LivesManager en la escena.");
        }

        // Asegurarse de que el texto esté desactivado al inicio
        if (scoreText != null)
        {
            scoreText.enabled = false; // Deshabilitar el renderizado del texto
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

        // Asegurarse de que livesManager esté asignado antes de calcular
        if (livesManager != null)
        {
            score = livesManager.lives * 10; // Por ejemplo: cada vida equivale a 10 puntos
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
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Puntuación: {score}";
        }
        else
        {
            Debug.LogError("El campo scoreText no está asignado.");
        }
    }

    // Mostrar la puntuación en pantalla por unos segundos y luego ocultarla
    private IEnumerator DisplayScoreTemporarily(float duration)
    {
        if (scoreText != null)
        {
            scoreText.enabled = true; // Habilitar el renderizado del texto
            yield return new WaitForSeconds(duration); // Esperar el tiempo especificado
            scoreText.enabled = false; // Deshabilitar el texto después
        }
    }
}
