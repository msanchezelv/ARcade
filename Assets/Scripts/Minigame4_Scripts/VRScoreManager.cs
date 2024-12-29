using System.Collections;
using TMPro;
using UnityEngine;

public class VRScoreManager : MonoBehaviour
{
    public static VRScoreManager Instance { get; private set; }  // Singleton para acceder a esta clase
    public TextMeshProUGUI scoreText;  // Campo para asignar el TextMeshPro desde Unity
    public int score = 0;             // Puntuación inicial
    private VRLivesManager vrLivesManager;  // Referencia al VRLivesManager
    private bool gameEnded = false;         // Controla si el juego ha terminado

    private void Awake()
    {
        // Asegura que solo haya una instancia de VRScoreManager en la escena
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        vrLivesManager = VRLivesManager.Instance;  // Obtener la referencia al VRLivesManager
        if (vrLivesManager == null)
        {
            Debug.LogError("ScoreManager: No se encontró el componente VRLivesManager en la escena.");
        }

        // Asegurarse de que el texto esté desactivado al inicio
        if (scoreText != null)
        {
            scoreText.enabled = false;  // Deshabilitar el renderizado del texto al principio
        }
        else
        {
            Debug.LogError("ScoreManager: No se ha asignado un TextMeshPro al campo scoreText.");
        }
    }

    // Método para calcular y mostrar la puntuación al final del minijuego
    public void CalculateAndShowFinalScore()
    {
        if (gameEnded) return;  // Evitar cálculos múltiples

        gameEnded = true;  // Marcar el fin del juego

        // Asegurarse de que vrLivesManager esté asignado antes de calcular
        if (vrLivesManager != null)
        {
            score = vrLivesManager.lives * 10;
            UpdateScoreText();  // Actualizar el texto con la puntuación
            Debug.Log($"Puntuación calculada: {score} (basado en {vrLivesManager.lives} vidas restantes)");
        }
        else
        {
            Debug.LogError("No se pudo calcular la puntuación porque el VRLivesManager es nulo.");
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
            scoreText.enabled = true;  // Habilitar el renderizado del texto
            yield return new WaitForSeconds(duration);  // Esperar el tiempo especificado
            scoreText.enabled = false;  // Deshabilitar el texto después
        }
    }
}
