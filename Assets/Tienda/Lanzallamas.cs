using UnityEngine;

public class Lanzallamas : MonoBehaviour
{
    public GameObject fuegoVFX; // El efecto de fuego (puede tener collider también)
    private bool disparando = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ActivarFuego(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ActivarFuego(false);
        }
    }

    void ActivarFuego(bool estado)
    {
        if (fuegoVFX != null)
        {
            fuegoVFX.SetActive(estado);
        }
        disparando = estado;
    }
}
