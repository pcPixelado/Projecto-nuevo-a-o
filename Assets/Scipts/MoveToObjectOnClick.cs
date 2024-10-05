using UnityEngine;
using UnityEngine.AI;

public class AntMovement : MonoBehaviour
{
    public string targetTag = "Target"; // Tag del objeto que la hormiga va a cargar
    public GameObject baseDestination; // El GameObject al que debe llevar el objeto
    public float baseSpeed = 3.5f; // Velocidad base de la hormiga
    public float maxSpeed = 10f; // Velocidad m�xima que puede alcanzar
    private NavMeshAgent agent; // El agente de navegaci�n
    public GameObject targetObject; // El objeto que la hormiga va a cargar (p�blico)
    private ObjectMovement objectMovementScript; // Referencia al script del objeto
    private bool isCarryingObject = false; // Indica si la hormiga ya est� cargando el objeto
    private Animator animator; // Referencia al componente Animator
    private bool isKnockedOver = false; // Saber si la hormiga est� patas arriba

    private GameController gameController; // Referencia al GameController

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = baseSpeed;
        animator = GetComponent<Animator>(); // Obtener el Animator de la hormiga
        gameController = FindObjectOfType<GameController>(); // Encontrar el GameController en la escena
    }

    void Update()
    {
        if (isKnockedOver) return; // Si est� patas arriba, no hacer nada m�s

        if (!isCarryingObject)
        {
            // Si la hormiga no est� cargando el objeto, busca y ve hacia el objetivo
            if (targetObject == null)
            {
                // Buscar un objeto con el tag correcto
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f);
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.CompareTag(targetTag))
                    {
                        targetObject = hitCollider.gameObject;
                        objectMovementScript = targetObject.GetComponent<ObjectMovement>();
                        if (objectMovementScript != null)
                        {
                            objectMovementScript.AddAnt(this); // A�adir esta hormiga al objeto
                        }
                        break;
                    }
                }
            }

            // Si tiene un objetivo, seguir movi�ndose hacia �l
            if (targetObject != null)
            {
                agent.SetDestination(targetObject.transform.position);

                // Si la hormiga ha llegado al objeto
                if (Vector3.Distance(transform.position, targetObject.transform.position) < 1f)
                {
                    // La hormiga "toma" el objeto
                    isCarryingObject = true;
                }
            }
        }
        else
        {
            // Si la hormiga est� cargando el objeto, ve hacia la base
            if (baseDestination != null)
            {
                agent.SetDestination(baseDestination.transform.position);

                // Mover el objeto cargado con la hormiga
                targetObject.transform.position = transform.position;

                // Si la hormiga ha llegado a la base
                if (Vector3.Distance(transform.position, baseDestination.transform.position) < 1f)
                {
                    // Soltar el objeto en la base (puedes agregar m�s l�gica aqu�)
                    isCarryingObject = false;
                    objectMovementScript.RemoveAnt(this); // La hormiga ya no est� empujando el objeto
                }
            }
        }
    }

    // Ajustar la velocidad de la hormiga seg�n la cantidad de hormigas que empujan el objeto
    public void AdjustSpeed(float newSpeed)
    {
        agent.speed = Mathf.Clamp(newSpeed, baseSpeed, maxSpeed); // Limitar la velocidad entre la base y el m�ximo
    }

    // M�todo para voltear la hormiga y detener la animaci�n
    public void KnockOver()
    {
        if (!isKnockedOver)
        {
            isKnockedOver = true;
            agent.isStopped = true; // Detener el movimiento del NavMeshAgent
            animator.enabled = false; // Detener la animaci�n
            // Girar la hormiga para ponerla patas arriba
            transform.Rotate(180f, 0f, 0f);

            // Informar al GameController que una hormiga ha sido derrotada
            if (gameController != null)
            {
                gameController.AntDefeated();
            }
        }
    }
}
