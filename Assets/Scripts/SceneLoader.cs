using UnityEngine;
using UnityEngine.SceneManagement; // Importa la librería de manejo de escenas

public class SceneLoader : MonoBehaviour
{
    // Esta función será llamada por el botón de Unity UI
    public void LoadLevelSelectScene(string sceneName)
    {
        // Carga la escena cuyo nombre se pasa como argumento.
        SceneManager.LoadScene(sceneName);
    }
}
