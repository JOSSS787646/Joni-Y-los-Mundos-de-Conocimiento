using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaLibroRojo : MonoBehaviour
{

    public float velocidad;
    public int danio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * velocidad * Vector2.right);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController playerController))
        {
            playerController.RecibeDanio(transform.position, danio);
            Destroy(gameObject);
        }
    }
}
