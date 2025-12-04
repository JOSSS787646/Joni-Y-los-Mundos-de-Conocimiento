using UnityEngine;

public class FinDelJuego : MonoBehaviour
{
    public static FinDelJuego instancia;
    public GameObject gameOverImage;

    private void Awake()
    {
        instancia = this;
    }

    public void Mostrar()
    {
        gameOverImage.SetActive(true);
    }
}
