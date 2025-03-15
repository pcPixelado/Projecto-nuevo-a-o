using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image barraVida; // Asigna la imagen de la barra en el inspector
    private float vidaActual = 1f; // 1 = 100% de vida

    void Start()
    {
        ActualizarBarra();
    }

    public void ReducirVida(float cantidad)
    {
        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0f, 1f); // Evita que baje de 0
        ActualizarBarra();
    }

    private void ActualizarBarra()
    {
        barraVida.fillAmount = vidaActual; // Ajusta la barra
    }
}
