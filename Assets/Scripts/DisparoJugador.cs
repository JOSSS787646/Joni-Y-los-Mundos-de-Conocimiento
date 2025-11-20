using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    public Transform puntoDisparo;
    public GameObject balaPrefab;
    public float tiempoEntreDisparos = 0.3f;
    public float tiempoEsperaDisparo = 0.1f;
    public float ultimoDisparo = 0;


    private PlayerController player;

    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Time.time > tiempoEntreDisparos + ultimoDisparo)
        {
            ultimoDisparo = Time.time;
            player.animator.SetTrigger("disparo");
            Invoke(nameof(Disparar), tiempoEsperaDisparo);
        }
    }

    private void Disparar()
    {
        GameObject bala = Instantiate(balaPrefab, puntoDisparo.position, Quaternion.identity);

        int direccion = transform.localScale.x > 0 ? 1 : -1;

        bala.GetComponent<BalaJugador>().SetDireccion(direccion);

    }
}
