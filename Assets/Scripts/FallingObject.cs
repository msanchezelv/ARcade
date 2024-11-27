using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public float speed = 10f;
    private LivesManager livesManager;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("¡El script de FallingObject ha iniciado correctamente!");
        livesManager = FindObjectOfType<LivesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Destruir el objeto si sale de la pantalla
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    // Detectar colisiones con el gato
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colisión detectada con: " + other.gameObject.name);

        if (other.CompareTag("Player")) // Si el objeto que colisiona es el gato
        {
            
            Debug.Log("Colisión con el Gato detectada! Restando una vida...");
            
            // Restar una vida
            if (livesManager != null)
            {
                livesManager.DecreaseLife();

                // Destruir el objeto al colisionar
                Destroy(gameObject);
            }

            
        }
    }
}
