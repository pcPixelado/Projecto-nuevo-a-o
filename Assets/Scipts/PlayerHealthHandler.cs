using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{
    public HealthBar healthBar; // Arrastra el objeto de la barra de vida en el inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "HormigaGrande")
        {
            healthBar.ReducirVida(0.25f); // Baja un 25% de la vida
        }
    }
}
