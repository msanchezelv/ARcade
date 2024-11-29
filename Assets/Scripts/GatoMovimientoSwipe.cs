using UnityEngine;

public class GatoMovimientoSwipe : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector3 escalaInicial;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        escalaInicial = transform.localScale;
    }

    void Update()
    {
        DetectarToque();
        ActualizarAnimacion();
    }

    void FixedUpdate()
    {
        RestringirMovimiento();
    }

    private void DetectarToque()
    {
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);

            if (toque.phase == TouchPhase.Began || toque.phase == TouchPhase.Moved)
            {
                Vector2 posicionToque = Camera.main.ScreenToWorldPoint(toque.position);

                if (posicionToque.x < rb.position.x)
                {
                    // Mover a la izquierda
                    rb.velocity = new Vector2(-velocidadMovimiento, rb.velocity.y);

                    // Hacer que el gato mire hacia la izquierda
                    if (transform.localScale.x > 0)
                    {
                        transform.localScale = new Vector3(-escalaInicial.x, escalaInicial.y, escalaInicial.z);
                    }
                }
                else
                {
                    // Mover a la derecha
                    rb.velocity = new Vector2(velocidadMovimiento, rb.velocity.y);

                    // Hacer que el gato mire hacia la derecha
                    if (transform.localScale.x < 0)
                    {
                        transform.localScale = new Vector3(escalaInicial.x, escalaInicial.y, escalaInicial.z);
                    }
                }
            }
        }
        else
        {
            rb.velocity = Vector2.zero; // Detener el movimiento cuando no hay toque
        }
    }

    private void ActualizarAnimacion()
    {
        animator.SetBool("isWalking", rb.velocity.x != 0); // Activar animación si hay movimiento horizontal
    }

    private void RestringirMovimiento()
    {
        Vector3 esquinaInferiorIzquierda = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 esquinaSuperiorDerecha = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        float limiteX = Mathf.Clamp(rb.position.x, esquinaInferiorIzquierda.x, esquinaSuperiorDerecha.x);
        float limiteY = Mathf.Clamp(rb.position.y, esquinaInferiorIzquierda.y, esquinaSuperiorDerecha.y);

        rb.position = new Vector2(limiteX, limiteY);
    }
}
