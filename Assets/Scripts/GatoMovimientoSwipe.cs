using UnityEngine;

public class GatoMovimientoSwipe : MonoBehaviour
{
    public float velocidadMovimiento = 5f; // Velocidad de movimiento del gato
    private Vector2 direccionMovimiento;   // Dirección de movimiento
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        // Obtener el Rigidbody2D y el Animator del gato
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        #if UNITY_EDITOR
        // Simular movimiento en el editor usando las flechas
        if (Input.GetKeyDown(KeyCode.RightArrow)) 
            direccionMovimiento = Vector2.right;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            direccionMovimiento = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            direccionMovimiento = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            direccionMovimiento = Vector2.down;
        else
            direccionMovimiento = Vector2.zero; // Detener movimiento si no se presiona ninguna tecla
        #endif

        // Actualizar la animación
        ActualizarAnimacion();
    }

    void FixedUpdate()
    {
        // Mover al gato
        MoverGato();
    }

    private void MoverGato()
    {
        if (direccionMovimiento != Vector2.zero) // Si hay dirección de movimiento
        {
            rb.MovePosition(rb.position + direccionMovimiento * velocidadMovimiento * Time.fixedDeltaTime);
        }
    }

    private void ActualizarAnimacion()
    {
        // Si el gato se mueve, activamos la animación de caminar
        if (direccionMovimiento != Vector2.zero)
        {
            animator.SetBool("isWalking", true); // Establecer isWalking a true
        }
        else
        {
            animator.SetBool("isWalking", false); // Establecer isWalking a false
        }
    }
}
