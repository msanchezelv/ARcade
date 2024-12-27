using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRManager : MonoBehaviour
{
    public static VRManager Instance { get; private set; }

    public GameObject mousePrefab;                  // Prefab del ratón que se va a ir creando a medida que se destruyen
    public GameObject rightHandPrefab;              // Prefab de la pata de gato dcha.
    public float spawnRange = 5f;                   // Tiempo que tarda en spawnear un ratón (cada 5 segundos/frames)
    public int totalMice = 10;                      // Ratones totales
    private int miceEliminated = 0;                 // Ratones eliminados
    private bool gameStarted = false;               // Variable para comprobar si se ha iniciado el juego
    private VRTimeManager vrTimeManager;            // Instancia el Controlador de tiempo
    public float raycastDistance = 10f;             // Raycast para ver la interacción con los objetos y el jugador
   

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
       if (IsXRDeviceActive())
        {
            Debug.Log("La cámara VR funciona OK");
        }
        else
        {
            Debug.LogError("La cámara VR no está funcionando.");
        }

    }

    void Update()
    {
        if (!gameStarted && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)) && IsXRDeviceActive())
        {
            StartGame();
            Debug.Log("VRManager: ¡El juego ha comenzado!");
        }
    }

    // Comienza el juego, inicia el contador de vidas y el temporizador total (los 100 segundos en este caso)
    private void StartGame()
    {
        gameStarted = true;
        Debug.Log("VRGameManager: Juego iniciado.");
        VRTimeManager.Instance.StartTimer();
        VRLivesManager.Instance.StartGame();
    }

    // Detecta los ratones al pasar la mano sobre ellos
    public void DetectMouse(GameObject handInstance)
    {
        Ray ray = new Ray(handInstance.transform.position, handInstance.transform.forward);
        RaycastHit hit;

        // Verifica si el raycast colisiona con un ratón
        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Mouse"))
            {
                Destroy(hit.collider.gameObject); // Destruye el ratón
                Debug.Log("Ratón destruido por raycast");
                miceEliminated++;
                VRTimeManager.Instance.MouseDestroyed();
            }
        }
    }

    // Genera ratones en posiciones aleatorias
    IEnumerator SpawnMice()
    {
        while (miceEliminated < totalMice)
        {
            if (vrTimeManager != null && !vrTimeManager.gameStarted)
            {
                yield return null; // Espera un cuadro antes de continuar.
                continue;
            }

            // Si el tiempo para el ratón ha terminado, reinicia el temporizador y resta una vida
            if (vrTimeManager.timeRemaining <= 0f)
            {
                VRLivesManager.Instance.DecreaseLife();
                Debug.Log("No se ha encontrado al ratón, generando nuevo ratón.");
                vrTimeManager.timeRemaining = vrTimeManager.timePerMouse; // Reinicia el tiempo por ratón
            }

            // Generar un ratón en una posición aleatoria
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnRange, spawnRange),
                1f,
                Random.Range(-spawnRange, spawnRange)
            );

            Instantiate(mousePrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Ratón generado en la posición: " + spawnPosition);

            // Espera hasta que el ratón sea destruido o el tiempo por ratón termine
            yield return new WaitForSeconds(vrTimeManager.timePerMouse);
        }
    }


    bool IsXRDeviceActive()
    {
        InputDeviceCharacteristics desiredCharacteristics = InputDeviceCharacteristics.HeadMounted | InputDeviceCharacteristics.TrackedDevice;
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, inputDevices);
        return inputDevices.Count > 0;
    }


}
