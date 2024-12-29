using System.Collections;
using Google.XR.Cardboard;
using UnityEngine;
using UnityEngine.XR.Management;

public class VRManager : MonoBehaviour
{
    public static VRManager Instance { get; private set; }

    public GameObject player;              // Jugador en la escena
    public Transform cameraTransform;      // Cámara del jugador (que sigue su movimiento)
    public GameObject mousePrefab;         // Prefab de ratón que aparecerá en la escena
    public Transform rightHand;            // Mano derecha del jugador (para la interacción)
    public float spawnRange = 5f;          // Rango en el que los ratones pueden aparecer
    public float spawnInterval = 2f;       // Intervalo de aparición de ratones
    public int totalMice = 10;             // Total de ratones en la escena
    private int miceEliminated = 0;        // Ratones eliminados
    private bool gameStarted = false;      // Control del estado del juego
    private VRTimeManager vrTimeManager;   // Referencia al VRTimeManager
    public float raycastDistance = 10f;    // Distancia máxima para el raycast
    public Google.XR.Cardboard.XRLoader xrLoader = (Google.XR.Cardboard.XRLoader)XRGeneralSettings.Instance.Manager.activeLoader;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        vrTimeManager = FindObjectOfType<VRTimeManager>(); // Encuentra el VRTimeManager en la escena
        if (vrTimeManager == null)
        {
            Debug.LogError("No se encontró VRTimeManager en la escena.");
        }

        xrLoader.Initialize();
        
        if (xrLoader != null && xrLoader.Initialize())
        {
            Debug.Log("XRLoader Initialized");
            xrLoader.Start();

            if (xrLoader.Start())
            {
                Debug.Log("XRLoader Started");
            }
        }
        else
        {
            Debug.LogError("Failed to initialize XRLoader.");
        }

    }

    private void OnDestroy()
    {
        // Detener XRLoader cuando se destruye
        if (xrLoader != null)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            Debug.Log("XRLoader stopped.");
        }
    }

    private void Update()
    {
        // Comienza el minijuego si se detecta algún toque o clic del mouse
        if (!gameStarted && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
        {
            gameStarted = true;
            StartGame();
            StartCoroutine(SpawnMice());
        }

        // Detección de ratones al mirar (raycast desde la cámara)
        DetectMice();
        
        if (gameStarted && miceEliminated >= totalMice)
        {
            vrTimeManager.EndGame("Has ganado!"); // Termina el juego si se eliminan todos los ratones
        }
    }

    private void StartGame()
    {
        gameStarted = true;
        Debug.Log("VRGameManager: Juego iniciado.");
        VRTimeManager.Instance.StartTimer();
        VRLivesManager.Instance.StartGame();
    }


    // Detectar ratones usando raycast desde la cámara
    private void DetectMice()
    {
        Ray ray = cameraTransform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition); // Crear el rayo desde la cámara

        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance))
        {
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Mouse"))
            {
                // Si el rayo toca un ratón, se destruye
                Destroy(hit.collider.gameObject);
                Debug.Log("Ratón destruido por raycast");
                vrTimeManager.MouseDestroyed();
                miceEliminated++;
                Debug.Log($"Ratones eliminados: {miceEliminated}/{totalMice}");
            }
        }
    }

    private IEnumerator SpawnMice()
    {
        // Generar ratones si no se han eliminado todos
        while (miceEliminated < totalMice)
        {
            if (vrTimeManager != null && !gameStarted)
            {
                yield break; // No se crean ratones si el juego no ha empezado
            }

            // Genera una posición aleatoria para el ratón
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnRange, spawnRange),
                1f,
                Random.Range(-spawnRange, spawnRange)                
            );
            
            Instantiate(mousePrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Ratón generado en la posición: " + spawnPosition);
            
            yield return new WaitForSeconds(spawnInterval);
        }
    }

}
