using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerMovement : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento
    public float range = 2f; // Rango de movimiento
    private Vector3 startPosition;

    private void Start()
    {
        // Posicion de inicio de la mano
        startPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        // Mueve el png de la mano de un lado a otro automaticamente
        float offset = Mathf.Sin(Time.time * speed) * range;
        transform.position = new Vector3(startPosition.x + offset, startPosition.y, startPosition.z);

        // Detecta si el jugador toca la pantalla
        if (Input.touchCount > 0)
        {
            gameObject.SetActive(false); // Desactiva el png
        }
    }
}
