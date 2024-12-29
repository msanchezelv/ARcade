using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public float speed = 7f;
    public LivesManager livesManager;

    // Start is called before the first frame update
    private void Start()
    {
        livesManager = FindObjectOfType<LivesManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Destruir el objeto si sale de la pantalla
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    // Detectar colisiones con el gato
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameManager.gameStarted)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().color = Color.red;            
            
            if (livesManager != null)
            {
                livesManager.DecreaseLife();
            }


            ObjectSpawner objectSpawner = FindObjectOfType<ObjectSpawner>();

            if (objectSpawner != null)
            {
                objectSpawner.SpawnObject();
            }

            Destroy(gameObject);

        }
    }
}
