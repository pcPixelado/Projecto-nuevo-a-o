using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image barraVida; // Arrastra la imagen de "Relleno" aquí en el Inspector
    private float vidaActual = 1f; // 100% de vida

    public void ReducirVida(float cantidad)
    {
        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0f, 1f); // Evita valores negativos
        barraVida.fillAmount = vidaActual; // Cambia el tamaño de la barra
    }
}
