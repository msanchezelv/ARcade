using UnityEngine;

public class MostrarInstrucciones : MonoBehaviour
{
    public GameObject mano;

    private void Start()
    {        
        mano.SetActive(false);
        MostrarInstruccionesJuego();        
    }

    private void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0)) // Detecta toque o click en la pantalla y esconde las instrucciones
        {
            OcultarInstrucciones();
        }
    }

    private void MostrarInstruccionesJuego()
    {        
        mano.SetActive(true);
    }

    private void OcultarInstrucciones()
    {
        mano.SetActive(false);

    }
}
