using UnityEngine;

public class MostrarInstrucciones : MonoBehaviour
{
    public GameObject mano;

    void Start()
    {        
        mano.SetActive(false);
        
        // Mostrar las instrucciones
        MostrarInstruccionesJuego();

        GameManager.gameStarted = false;
    }

    void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0)) // Detecta toque o click en la pantalla
        {
            // Ocultar las instrucciones cuando se toca la pantalla
            OcultarInstrucciones();

            GameManager.gameStarted = true;
        }
    }

    void MostrarInstruccionesJuego()
    {        
        mano.SetActive(true);
    }

    void OcultarInstrucciones()
    {
        mano.SetActive(false);

    }
}
