using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    public TextMeshProUGUI livesText;  // Texto para mostrar las vidas
    public int lives = 7;              // N�mero inicial de vidas
    private GameManager gameManager;   // Referencia al GameManager
    private PlayerManager playerManager;
    private bool gameStarted = false;  // Ppara controlar el inicio del juego

    void Start()
    {
        UpdateLivesText();  // Actualizar el texto de las vidas al inicio
    }

    void Update()
    {
        // Iniciar el juego cuando el jugador toque la pantalla
        if (!gameStarted && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
        {
            gameStarted = true;
            Debug.Log("LivesManager: ¡El juego ha comenzado!");
        }
    }

    public void DecreaseLife()
    {
        if (gameStarted && lives > 0)
        {
            lives--;
            UpdateLivesText();            
        }

        if (lives <= 0)
        {
            Debug.Log("Vidas agotadas, cambiando turno...");
            PlayerManager.Instance.NextTurn();
        }

    }


    // Actualizar el texto con las vidas actuales
    void UpdateLivesText()
    {
        livesText.text = lives.ToString();
    }
}
