using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonJugar : MonoBehaviour
{
    // Nombre de la escena a la que quieres ir
    public string nombreEscena;

    public void Jugar()
    {
        // Carga la escena especificada en el campo 'nombreEscena'
        SceneManager.LoadScene(nombreEscena);
    }
}
