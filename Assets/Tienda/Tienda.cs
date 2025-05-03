using UnityEngine;

public class Tienda : MonoBehaviour
{
    public GameObject lanzallamas; // objeto que ya está en el jugador
    private bool entregado = false;

    public void ComprarLanzallamas()
    {
        if (!entregado)
        {
            lanzallamas.SetActive(true); // ahora lo puedes usar
            entregado = true;
        }
    }
}
