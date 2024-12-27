using System.Collections;
using TMPro;
using UnityEngine;

public class VRLivesManager : MonoBehaviour
{
    public static VRLivesManager Instance { get; private set; }
    public TextMeshProUGUI livesText;  // Texto vidas
    public int lives = 7;              // Número de vidas
    private bool gameStarted = false;  // Para controlar el inicio del juego

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        if (gameStarted)
        {
            UpdateLivesText();
        }
    }

    void Update()
    {
    
    }

    public void StartGame()
    {
        gameStarted = true;
        Debug.Log("Contador de vidas iniciado");
        UpdateLivesText();
    }

    // Quitar una vida
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
            VRTimeManager.Instance.EndGame("Sin vidas");  // El jeugo termina cuando las vidas llegan a 0
        }
    }

    // Actualiza las vidas que quedan
    void UpdateLivesText()
    {
        livesText.text = lives.ToString();
    }
}
