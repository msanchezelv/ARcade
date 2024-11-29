using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    public TextMeshProUGUI livesText;  // Texto para mostrar las vidas
    public int lives = 7;              // Número inicial de vidas
    private GameManager gameManager;   // Referencia al GameManager
    private PlayerManager playerManager;
    private bool gameStarted = false;  // Bandera para controlar el inicio del juego

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();  // Obtener referencia al GameManager
        UpdateLivesText();  // Actualizar el texto de las vidas al inicio
    }

    void Update()
    {
        // Iniciar el juego cuando el jugador toque la pantalla
        if (!gameStarted && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
        {
            gameStarted = true;
            Debug.Log("¡El juego ha comenzado!");
        }
    }

    // Método para restar vidas cuando sea necesario
    public void DecreaseLife()
    {
        if (gameStarted && lives > 0)
        {
            lives--;
            UpdateLivesText();
            Debug.Log("Vidas restantes: " + lives);
        }

        if (lives <= 0)
        {
            Debug.Log("Vidas agotadas, cambiando de turno...");
            playerManager.NextTurn();
        }
    }


    // Actualizar el texto con las vidas actuales
    void UpdateLivesText()
    {
        livesText.text = lives.ToString();
    }
}
