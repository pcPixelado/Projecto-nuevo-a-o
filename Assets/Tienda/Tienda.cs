using UnityEngine;
using UnityEngine.UI;

public class Tienda : MonoBehaviour
{
    public GameObject lanzallamas; // Objeto que ya está en el jugador
    public Button button1;         // Botón UI asignado desde el inspector
    private bool entregado = false;

    void Start()
    {
        lanzallamas.SetActive(false); // Se oculta al inicio del juego
        button1.onClick.AddListener(ComprarLanzallamas);
    }

    public void ComprarLanzallamas()
    {
        if (!entregado)
        {
            lanzallamas.SetActive(true); // Se activa al comprar
            entregado = true;
        }
    }
}
