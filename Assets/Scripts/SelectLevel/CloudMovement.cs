using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    [Tooltip("Velocidad a la que se mueve el grupo de nubes (unidades por segundo).")]
    public float speed = 1.5f;

    [Tooltip("Ancho total del grupo de nubes. (CRUCIAL: 22.44)")]
    // Usamos el valor aproximado del ancho total del área visible (11.22 * 2)
    public float objectWidth = 22.44f;

    // Almacenamos el Transform para mejor rendimiento
    private Transform objectTransform;

    void Awake()
    {
        objectTransform = transform;
    }

    void Update()
    {
        // 1. Mover el grupo de nubes hacia la izquierda (dirección del movimiento).
        objectTransform.Translate(Vector3.left * speed * Time.deltaTime);

        // 2. Comprobar si el grupo ha salido completamente de la posición inicial.
        // El punto de reinicio es la mitad del ancho del objeto, ya que el centro de la escena es X=0.
        float resetPoint = -objectWidth;

        // Si el objeto actual está completamente fuera de la vista a la izquierda...
        if (objectTransform.position.x < resetPoint)
        {
            // 3. Teletransportar el objeto exactamente 2 veces su ancho hacia la derecha.
            // Esto lo coloca justo después del otro objeto que está visible, 
            // manteniendo el bucle infinito sin interrupciones.

            Vector3 jumpOffset = new Vector3(objectWidth * 2f, 0, 0);
            objectTransform.position += jumpOffset;
        }
    }
}