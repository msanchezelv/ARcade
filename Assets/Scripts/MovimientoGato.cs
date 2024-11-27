using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoGato : MonoBehaviour
{
    public float velocidad = 2f;

    public Animator animator;

    private void Start()
    {
         Debug.Log("¡El script de MovimientoGato ha iniciado correctamente!");
    }
    void Update()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");

        Vector3 desplazamiento = new Vector3(movimientoHorizontal, 0, 0) * velocidad * Time.deltaTime;
        transform.Translate(desplazamiento);
        animator.SetFloat("Velocidad", Mathf.Abs(movimientoHorizontal));
    }

}
