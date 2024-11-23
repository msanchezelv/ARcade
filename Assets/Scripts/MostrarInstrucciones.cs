using UnityEngine;

public class MostrarInstrucciones : MonoBehaviour
{
    public GameObject flechaIzquierda;
    public GameObject flechaDerecha;
    public GameObject mano;

    void Start()
    {
        // Desactivar las imágenes al principio
        flechaIzquierda.SetActive(false);
        flechaDerecha.SetActive(false);
        mano.SetActive(false);
        
        // Mostrar las instrucciones
        MostrarInstruccionesJuego();
    }

    void Update()
    {
        // Detectar toque en pantalla (funciona tanto en móvil como en el editor)
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0)) // Detecta toque o click en la pantalla
        {
            // Ocultar las instrucciones cuando se toca la pantalla
            OcultarInstrucciones();
        }
    }

    void MostrarInstruccionesJuego()
    {
        // Mostrar las flechas y la mano
        flechaIzquierda.SetActive(true);
        flechaDerecha.SetActive(true);
        mano.SetActive(true);
    }

    void OcultarInstrucciones()
    {
        // Ocultar las flechas y la mano
        flechaIzquierda.SetActive(false);
        flechaDerecha.SetActive(false);
        mano.SetActive(false);

        // Aquí puedes iniciar el minijuego o realizar otras acciones si lo deseas
    }
}
