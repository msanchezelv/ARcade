using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] fallingObjectPrefabs; // Array de prefabs de objetos
    public float spawnInterval = 1f; // Intervalo de generación
    private float spawnRangeX; // Rango horizontal dinámico

    void Start()
    {
        // Obtener el límite de la pantalla en el eje X usando la cámara
        Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenBottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));

        // Definir el rango horizontal basado en los límites de la pantalla
        spawnRangeX = Mathf.Abs(screenBottomRight.x - screenBottomLeft.x) / 2;

        // Iniciar el spawner
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }

    public void SpawnObject()
    {
        // Seleccionar un prefab aleatorio de la lista
        int randomIndex = Random.Range(0, fallingObjectPrefabs.Length);
        GameObject randomPrefab = fallingObjectPrefabs[randomIndex];             

        // Generar en una posición aleatoria dentro del rango visible
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnRangeX, spawnRangeX), // Dentro del rango horizontal
            transform.position.y,                   // Posición vertical fija
            0f
        );

        Instantiate(randomPrefab, spawnPosition, Quaternion.identity);

    }
       
}
