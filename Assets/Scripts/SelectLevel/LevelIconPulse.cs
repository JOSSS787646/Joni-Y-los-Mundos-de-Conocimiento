using UnityEngine;

public class LevelIconPulse : MonoBehaviour
{
    [Header("Configuración del Pulso")]
    [Tooltip("Escala máxima que alcanzará el icono (ej. 1.2 para 120%).")]
    // **NOTA: Esta variable 'maxScale' es el FACTOR de escala (un número simple).**
    public float scaleFactor = 1.2f;

    [Tooltip("Duración completa de un ciclo de pulso (de pequeño a grande y vuelta).")]
    public float pulseTime = 1.0f;

    // Escala inicial del icono (Vector3)
    private Vector3 originalScale;

    // Almacena el tiempo para calcular la posición en el ciclo
    private float time;

    void Start()
    {
        // Guardar la escala inicial del objeto (Vector3)
        originalScale = transform.localScale;
    }

    void Update()
    {
        // 1. Acumular el tiempo transcurrido en cada frame
        time += Time.deltaTime;

        // 2. Usar la función Seno (Sine) para crear un valor que oscila suavemente entre -1 y 1
        float scaleSin = Mathf.Sin(time * (2f * Mathf.PI / pulseTime));

        // 3. Remapear el factor Seno de [-1, 1] a [0, 1]
        // 't' representa el progreso de la animación de 0 a 1
        float t = (scaleSin + 1f) * 0.5f;

        // 4. DEFINIR las escalas mínima y máxima (usando la variable originalScale)
        Vector3 minScale = originalScale;
        // CORRECTO: Multiplicar la escala original por el FACTOR de escala
        Vector3 maxPulseScale = originalScale * scaleFactor;

        // 5. Interpolar (Lerp) suavemente entre la escala mínima y la máxima
        transform.localScale = Vector3.Lerp(minScale, maxPulseScale, t);
    }
}