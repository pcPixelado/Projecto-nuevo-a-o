using UnityEngine;
using UnityEngine.AI;

public class ObjectFollowerWithAvoidance : MonoBehaviour
{
    public Transform target; // Objeto a perseguir
    public float speed = 3f; // Velocidad de movimiento
    private NavMeshAgent agent;

    void Start()
    {
        // Obtener el componente NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    void Update()
    {
        if (target != null)
        {
            // Establecer la posición objetivo para el NavMeshAgent
            agent.SetDestination(target.position);
        }
    }
}
