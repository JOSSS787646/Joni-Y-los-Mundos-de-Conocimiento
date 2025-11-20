using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    public float velocidad;
    public int danio;
    private int direccion = 1;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * velocidad * direccion * Vector2.right);
    }

    public void SetDireccion(int dir)
    {
        direccion = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Escudo"))
        {
            // La bala se destruye y no hace daño
            Destroy(gameObject);
            return;
        }

        if (collision.TryGetComponent(out PlayerController playerController))
        {
            playerController.RecibeDanio(transform.position, danio);
            Destroy(gameObject);
        }
    }
}
