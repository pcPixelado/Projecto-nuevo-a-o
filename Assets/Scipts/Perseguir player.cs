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

            // Comprobar si el agente se está moviendo
            if (agent.velocity.sqrMagnitude > 0.1f) 
            {
                // Rotar hacia la dirección del movimiento
                Vector3 direccionMovimiento = agent.velocity.normalized;
                Quaternion nuevaRotacion = Quaternion.LookRotation(direccionMovimiento);
                transform.rotation = Quaternion.Slerp(transform.rotation, nuevaRotacion, Time.deltaTime * 10f);
            }
        }
    }
}
