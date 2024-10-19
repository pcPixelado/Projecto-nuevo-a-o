using UnityEngine;

public class KillerObject : MonoBehaviour
{
    private Vector3 lastPosition; // Posición en el último frame
    private bool isMoving; // Saber si el objeto está en movimiento

    void Start()
    {
        lastPosition = transform.position; // Inicializar con la posición actual
    }

    void Update()
    {
        // Calcular si el objeto se ha movido desde el último frame
        isMoving = Vector3.Distance(transform.position, lastPosition) > 0.01f; // Comparar posiciones con un pequeño margen
        lastPosition = transform.position; // Actualizar la última posición
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verificar si el objeto está en movimiento antes de hacer algo
        if (isMoving)
        {
            // Si colisionamos con un objeto que tiene el script AntMovement (una hormiga)
            AntMovement ant = collision.gameObject.GetComponent<AntMovement>();
            if (ant != null)
            {
                ant.KnockOver(); // Voltear la hormiga y detenerla
            }
        }
    }

    // Alternativamente, si quieres usar un Trigger en lugar de colisiones
    void OnTriggerEnter(Collider other)
    {
        if (isMoving)
        {
            AntMovement ant = other.gameObject.GetComponent<AntMovement>();
            if (ant != null)
            {
                ant.KnockOver(); // Voltear la hormiga y detenerla
            }
        }
    }
}
