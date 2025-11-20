using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisparoEnemigo : MonoBehaviour
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
    private EnemyController enemy;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemy = GetComponentInParent<EnemyController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null || player.muerto)
            return;

        Vector2 direccionDisparo = new Vector2(enemy.direccion, 0);

        jugadorEnRango = Physics2D.Raycast(
            controladorDisparo.position,
            direccionDisparo,
            distanciaLinea,
            capaJugador
        );

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
        GameObject bala = Instantiate(balaEnemigo, controladorDisparo.position, Quaternion.identity);

        // hacer que la bala se mueva hacia la dirección del enemigo
        bala.GetComponent<BalaEnemigo>().SetDireccion(enemy.direccion);
    }

    private void OnDrawGizmos()
    {
        if (enemy != null)
        {
            Gizmos.DrawLine(controladorDisparo.position,
                            controladorDisparo.position + new Vector3(enemy.direccion * distanciaLinea, 0, 0));
        }

    }
}
