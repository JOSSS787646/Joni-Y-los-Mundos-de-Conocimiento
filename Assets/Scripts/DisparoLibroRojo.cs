using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoLibroRojo : MonoBehaviour
{
    public Transform controladorDisparo;

    public float distanciaLinea;

    public LayerMask capaJugador;

    public bool jugadorEnRango;

    public GameObject balaEnemigo;

    public float tiempoEntreDisparos;
    public float tiempoUltimoDisparo;
    public float tiempoEsperaDisparo;

    public Animator animator;
    private PlayerController player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (player == null || player.muerto)
            return;

        jugadorEnRango = Physics2D.Raycast(controladorDisparo.position, transform.right, distanciaLinea, capaJugador);
        
        if (jugadorEnRango) 
        {
            if (Time.time > tiempoEntreDisparos + tiempoUltimoDisparo)
            {
                tiempoUltimoDisparo = Time.time;
                animator.SetTrigger("Disparar");
                Invoke(nameof(Disparar), tiempoEsperaDisparo);
            }
        }
    }

    private void Disparar()
    {
        Instantiate(balaEnemigo, controladorDisparo.position, controladorDisparo.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(controladorDisparo.position, controladorDisparo.position + transform.right * distanciaLinea);
    }
}
