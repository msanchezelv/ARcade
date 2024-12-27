using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntermediateScreenManager : MonoBehaviour
{
    public TextMeshProUGUI playerText; // Referencia al texto de jugador
    public GameObject readyButton;    // Referencia al botón "Listo para jugar"

    public static IntermediateScreenManager Instance { get; private set; }


    private void Start()
    {
        Debug.Log($"IntermediateScreenManager cargado. Jugador actual: {PlayerManager.Instance.currentPlayer}");
        UpdatePlayerText(PlayerManager.Instance.currentPlayer); // Actualizar el texto al cargar la escena

        if (PlayerManager.Instance != null)
        {
            UpdatePlayerText(PlayerManager.Instance.currentPlayer); // Actualiza texto con el jugador actual
        }
        else
        {
            Debug.LogError("PlayerManager no está disponible.");
        }
    }

    public void UpdatePlayerText(int currentPlayer)
    {
        if (playerText != null)
        {
            Debug.Log($"[IntermediateScreenManager_UpdatePlayerText] Actualizando texto: Jugador {currentPlayer}");
            playerText.text = $"Jugador {currentPlayer}";
        }
        else
        {
            Debug.LogError("[UpdatePlayerText] No se encontró el componente TextMeshProUGUI.");
        }
    }




    private void EnsureButtonAppearance()
    {
        if (readyButton == null)
        {
            var buttonImage = readyButton.GetComponent<UnityEngine.UI.Image>();
            if (buttonImage != null)
            {
                buttonImage.color = new Color(1f, 0.780f, 0.443f, 1f);  // Cambiar el color del botón
            }
        }
    }

    public void StartMinigame()
    {
        if (PlayerManager.Instance != null)
        {
            string sceneName = "Minigame" + PlayerManager.Instance.currentMinigame;
            SceneManager.LoadScene(sceneName);
            Debug.Log($"Cargando escena: {sceneName}");
        }
        else
        {
            Debug.LogError("PlayerManager no está disponible para iniciar el minijuego.");
        }
    }

    //public void StartMinigame()
    //{
    //    if (PlayerManager.Instance != null)
    //    {
    //        int minigameIndex = PlayerManager.Instance.currentMinigame;
    //        Debug.Log("Jugador actual al iniciar el minijuego: " + PlayerManager.Instance.currentPlayer);  // Verificar jugador actual
    //        SceneManager.LoadScene("Minigame" + minigameIndex);
    //    }
    //}
}
