using UnityEngine;
using System.Collections; // Necesario para las Coroutines

public class PlayButtonPulse : MonoBehaviour
{
    [Header("Configuración del Pulso")]
    [Tooltip("Escala máxima que alcanzará el botón (ej. 1.1 para 110%).")]
    public float maxScale = 1.1f;

    [Tooltip("Duración de cada fase del pulso (de pequeño a grande o viceversa).")]
    public float pulseDuration = 0.5f;

    [Tooltip("Retraso entre pulsos (tiempo de espera antes de que comience el siguiente pulso).")]
    public float pulseDelay = 0.2f;

    // Escala inicial del botón
    private Vector3 originalScale;

    void Start()
    {
        // Guardar la escala inicial del botón
        originalScale = transform.localScale;

        // Iniciar la coroutine de pulso
        StartCoroutine(PulseEffect());
    }

    IEnumerator PulseEffect()
    {
        // Bucle infinito para que el pulso se repita
        while (true)
        {
            // FASE 1: Hacer el botón más grande (de originalScale a maxScale)
            float timer = 0f;
            while (timer < pulseDuration)
            {
                timer += Time.deltaTime;
                float progress = timer / pulseDuration; // Progreso de 0 a 1
                transform.localScale = Vector3.Lerp(originalScale, originalScale * maxScale, progress);
                yield return null; // Esperar al siguiente frame
            }
            transform.localScale = originalScale * maxScale; // Asegurar que alcanza el tamaño máximo

            // FASE 2: Hacer el botón más pequeño (de maxScale a originalScale)
            timer = 0f;
            while (timer < pulseDuration)
            {
                timer += Time.deltaTime;
                float progress = timer / pulseDuration; // Progreso de 0 a 1
                transform.localScale = Vector3.Lerp(originalScale * maxScale, originalScale, progress);
                yield return null; // Esperar al siguiente frame
            }
            transform.localScale = originalScale; // Asegurar que vuelve al tamaño original

            // Esperar un momento antes del siguiente pulso
            yield return new WaitForSeconds(pulseDelay);
        }
    }
}