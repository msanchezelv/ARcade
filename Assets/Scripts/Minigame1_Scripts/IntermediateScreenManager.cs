using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntermediateScreenManager : MonoBehaviour
{
    public TextMeshProUGUI playerText;
    public GameObject readyButton;
    public static IntermediateScreenManager Instance { get; private set; }


    private void Start()
    {
        Debug.Log($"IntermediateScreenManager cargado. Jugador actual: {PlayerManager.Instance.currentPlayer}");
        UpdatePlayerText(PlayerManager.Instance.currentPlayer); // Actualizar el texto al cargar la escena

        if (PlayerManager.Instance != null)
        {
            UpdatePlayerText(PlayerManager.Instance.currentPlayer); // Actualiza eyl texto con el jugador actual
        }
        else
        {
            Debug.LogError("PlayerManager no esta disponible.");
        }
    }

    public void UpdatePlayerText(int currentPlayer)
    {
        if (playerText != null)
        {
            playerText.text = $"Jugador {currentPlayer}";
        }
        else
        {
            Debug.LogError("[UpdatePlayerText] No se encontro el componente TextMeshProUGUI.");
        }
    }

    // Sin este método el btón sale transparente, no borrar
    private void EnsureButtonAppearance()
    {
        if (readyButton == null)
        {
            var buttonImage = readyButton.GetComponent<UnityEngine.UI.Image>();
            if (buttonImage != null)
            {
                buttonImage.color = new Color(1f, 0.780f, 0.443f, 1f);
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
            Debug.LogError("PlayerManager no esta disponible");
        }
    }
}
