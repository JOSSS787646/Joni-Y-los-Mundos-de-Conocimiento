using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaJugador : MonoBehaviour
{
    public float velocidad = 8f;
    public int danio = 1;
    private int direccion = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * velocidad * direccion * Time.deltaTime);
    }

    public void SetDireccion(int dir)
    {
        direccion = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyController enemy))
        {
            enemy.RecibeDanio(transform.position, danio);
            Destroy(gameObject);
        }
    }
}
