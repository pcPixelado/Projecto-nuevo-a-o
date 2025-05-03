using UnityEngine;

public class Fuego : MonoBehaviour
{
    public float tiempoVida = 2f;

    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo"))
        {
            Debug.Log("¡Impacto en " + other.name + "!");
            // Aquí puedes hacer daño, destruir el enemigo, etc.
        }
    }
}
