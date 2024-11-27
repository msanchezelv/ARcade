using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    public int lives = 7;

    void Start()
    {
        Debug.Log("Se ha iniciado el contador de vidas");
        UpdateLivesText();
    }

    public void DecreaseLife()
    {
        if (lives > 0)
        {
            lives--;
            UpdateLivesText();
        }
    }

    // Update is called once per frame
    void UpdateLivesText()
    {
        livesText.text = lives.ToString(); // Actualiza el texto con las vidas actuales
    }
}
