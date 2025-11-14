using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5.0f;
    public float speed = 2.0f;
    public float fuerzaRebote = 5f;
    public int vida = 3;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool enMovimiento;
    private bool muerto;
    private bool recibiendoDanio;
    private bool playerVivo;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerVivo = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerVivo && !muerto)
        {
            Movimiento();
        }

        animator.SetBool("enMovimiento", enMovimiento);
        animator.SetBool("muerto", muerto);
    }

    private void Movimiento()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            movement = new Vector2(direction.x, 0);

            enMovimiento = true;
        }
        else
        {
            movement = Vector2.zero;
            enMovimiento = false;
        }

        if (!recibiendoDanio)
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direccionDanio = new Vector2(transform.position.x, 0);

            PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();

            playerScript.RecibeDanio(direccionDanio, 1);
            playerVivo = !playerScript.muerto;

            if (!playerVivo)
            {
                enMovimiento = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lapiz"))
        {
            Vector2 direccionDanio = new Vector2(collision.gameObject.transform.position.x, 0);
            RecibeDanio(direccionDanio, 1);
        }
    }

    public void RecibeDanio(Vector2 direction, int cantDanio)
    {
        if (!recibiendoDanio)
        {
            vida -= cantDanio;
            recibiendoDanio = true;
            if(vida <= 0)
            {
                muerto = true;
                enMovimiento = false;
            }
            else
            {
                Vector2 rebote = new Vector2(transform.position.x - direction.x, 0.2f).normalized;
                rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
                StartCoroutine(DesactivaDanio());
            }
                
        }
    }

    IEnumerator DesactivaDanio()
    {
        yield return new WaitForSeconds(0.4f);
        recibiendoDanio = false;
        rb.velocity = Vector2.zero;
    }

    public void DeleteBody()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
