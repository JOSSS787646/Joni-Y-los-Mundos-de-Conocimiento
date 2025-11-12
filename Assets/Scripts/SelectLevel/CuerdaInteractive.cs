using UnityEngine;

public class CuerdaInteractive : MonoBehaviour
{
    [Header("Configuración de Velocidad")]
    [Tooltip("Velocidad normal de la cuerda (ej: 0.5 para que sea lenta).")]
    public float velocidadNormal = 0.5f;

    [Tooltip("Velocidad al pasar el mouse (ej: 2.0 para que acelere notablemente).")]
    public float velocidadInteractiva = 2.0f;

    // Referencia al componente Animator
    private Animator cuerdaAnimator;

    void Start()
    {
        // Obtener el componente Animator del objeto al inicio
        cuerdaAnimator = GetComponent<Animator>();

        // Establecer la velocidad base lenta
        cuerdaAnimator.speed = velocidadNormal;
    }

    // Función que se llama automáticamente cuando el mouse ENTRA en el Collider 2D
    void OnMouseEnter()
    {
        // Acelerar la animación
        if (cuerdaAnimator != null)
        {
            cuerdaAnimator.speed = velocidadInteractiva;
        }
    }

    // Función que se llama automáticamente cuando el mouse SALE del Collider 2D
    void OnMouseExit()
    {
        // Regresar a la velocidad normal
        if (cuerdaAnimator != null)
        {
            cuerdaAnimator.speed = velocidadNormal;
        }
    }
}