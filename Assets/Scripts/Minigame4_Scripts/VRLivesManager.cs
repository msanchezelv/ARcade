using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VRLivesManager : MonoBehaviour
{
    public static VRLivesManager Instance { get; private set; }
    public TextMeshProUGUI livesText;  // Texto para mostrar las vidas
    public int lives = 7;              // Número inicial de vidas
    private bool gameStarted = false;  // Para controlar el inicio del juego

    private void Awake()
    {
        // Asegura que solo haya una instancia de VRLivesManager en la escena
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        UpdateLivesText();  // Actualiza el texto con el número inicial de vidas
    }


    public void DecreaseLife()
    {
        if (gameStarted && lives > 0)
        {
            lives--;
            UpdateLivesText();  // Actualiza el texto cada vez que se pierde una vida
        }

        // Si no hay vidas restantes, terminar el juego
        if (lives <= 0)
        {
            Debug.Log("Vidas agotadas, cambiando turno...");
            VRTimeManager.Instance.EndGame("Te has quedado sin vidas");  // Finaliza el juego si no hay vidas
        }
    }

    // Actualiza el texto de las vidas en la UI
    private void UpdateLivesText()
    {
        livesText.text = lives.ToString();
    }

    internal void StartGame()
    {
        gameStarted = true;
        UpdateLivesText();
    }
}
