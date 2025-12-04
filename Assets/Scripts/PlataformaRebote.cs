using UnityEngine;

public class PlataformaHundimiento : MonoBehaviour
{
    public float hundimiento = 0.2f;       // cuánto se hunde
    public float velocidadMovimiento = 8f; // velocidad del movimiento

    private Vector3 posicionInicial;
    private Vector3 posicionHundida;
    private bool jugadorEncima = false;

    void Start()
    {
        posicionInicial = transform.position;
        posicionHundida = posicionInicial + new Vector3(0, -hundimiento, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            jugadorEncima = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            jugadorEncima = false;
        }
    }

    void Update()
    {
        if (jugadorEncima)
        {
            // Hundirse hasta la posición hundida
            transform.position = Vector3.Lerp(
                transform.position,
                posicionHundida,
                Time.deltaTime * velocidadMovimiento
            );
        }
        else
        {
            // Regresar a la posición original
            transform.position = Vector3.Lerp(
                transform.position,
                posicionInicial,
                Time.deltaTime * velocidadMovimiento
            );
        }
    }
}
