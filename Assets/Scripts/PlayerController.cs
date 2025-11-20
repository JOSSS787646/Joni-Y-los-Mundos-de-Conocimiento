using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 5f;
    public int vida = 5;

    public float fuerzaSalto = 5f;
    public float fuerzaRebote = 5f;
    public float longitudRayCast = 0.1f;
    public LayerMask capaSuelo;

    private bool enSuelo;
    private bool recibiendoDanio;
    private bool atacando;
    public bool muerto;

    private Rigidbody2D rb;

    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!muerto)
        {
            if (!atacando)
            {
                Movimiento();

                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRayCast, capaSuelo);
                enSuelo = hit.collider != null;

                if (enSuelo && Input.GetKeyDown(KeyCode.Space) && !recibiendoDanio)
                {
                    rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) && !atacando && enSuelo)
        {
            Atacando();
        }

        animator.SetBool("ensuelo", enSuelo);
        animator.SetBool("recibeDanio", recibiendoDanio);
        animator.SetBool("Atacando", atacando);
        animator.SetBool("muerto", muerto);
    }

    private void Movimiento()
    {
        float velocidadX = Input.GetAxis("Horizontal") * Time.deltaTime * velocidad;

        animator.SetFloat("movement", velocidadX * velocidad);

        if (velocidadX < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        if (velocidadX > 0)
            transform.localScale = new Vector3(1, 1, 1);

        Vector3 posicion = transform.position;

        if (!recibiendoDanio)
            transform.position = new Vector3(velocidadX + posicion.x, posicion.y, posicion.z);
    }

    public void RecibeDanio(Vector2 direction, int cantDanio)
    {
        if (!recibiendoDanio)
        {
            recibiendoDanio = true;
            vida -= cantDanio;

            if (vida <= 0)
            {
                muerto = true;
            }
            else
            {
                Vector2 rebote = new Vector2(transform.position.x - direction.x, 0.2f).normalized;
                rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
            }
        }
    }

    public void DesactivaDanio()
    {
        recibiendoDanio = false;
        rb.velocity = Vector2.zero;
    }

    public void Atacando()
    {
        atacando = true;
    }

    public void DesactivaAtaque()
    {
        atacando = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRayCast);
    }
}
