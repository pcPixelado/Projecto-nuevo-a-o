using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AntMovement : MonoBehaviour
{
    public string targetTag = "AZUCAR"; // Cambiado a "AZUCAR" como destino por defecto
    public GameObject baseDestination;
    public float baseSpeed = 3.5f;
    public float maxSpeed = 10f;
    public float checkInterval = 0.5f; // Intervalo de tiempo para verificar movimiento
    public float minDistance = 0.5f;  // Distancia mínima para considerar movimiento

    private NavMeshAgent agent;
    public GameObject targetObject;
    private bool isCarryingObject = false;
    private Animator animator;
    private bool isKnockedOver = false;

    private GameController gameController;

    private Vector3 lastPosition;
    private float lastCheckTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = baseSpeed;
        animator = GetComponent<Animator>();
        gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        if (isKnockedOver) return;

        if (!isCarryingObject)
        {
            if (targetObject == null)
            {
                // Buscar el objeto más cercano con el tag "AZUCAR"
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f);
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.CompareTag(targetTag))
                    {
                        targetObject = hitCollider.gameObject;
                        break;
                    }
                }
            }

            if (targetObject != null)
            {
                agent.SetDestination(targetObject.transform.position);
            }
        }
        else
        {
            if (baseDestination != null)
            {
                agent.SetDestination(baseDestination.transform.position);

                if (Vector3.Distance(transform.position, baseDestination.transform.position) < 1f)
                {
                    isCarryingObject = false;
                }
            }
        }
    }

    public void AdjustSpeed(float newSpeed)
    {
        agent.speed = Mathf.Clamp(newSpeed, baseSpeed, maxSpeed);
    }

    public void KnockOver()
    {
        if (!isKnockedOver)
        {
            isKnockedOver = true;
            agent.isStopped = true;
            animator.enabled = false;

            transform.Rotate(0f, 90f, 0f);

            if (gameController != null)
            {
                gameController.AntDefeated();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("antihormiga"))
        {
            if (HasObjectMoved(other.gameObject))
            {
                Destroy(gameObject);
            }
        }

        if (other.CompareTag("Target"))
        {
            Debug.Log("Colisión con el objeto objetivo. Cambiando de escena...");
            SceneManager.LoadScene("MenuNiveles");
        }
    }

    private bool HasObjectMoved(GameObject obj)
    {
        float currentTime = Time.time;

        // Si no ha pasado suficiente tiempo, no comprobar movimiento
        if (currentTime - lastCheckTime < checkInterval)
        {
            return false;
        }

        // Calcular distancia recorrida desde la última posición conocida
        Vector3 currentPosition = obj.transform.position;
        float distanceMoved = Vector3.Distance(currentPosition, lastPosition);

        // Actualizar última posición y tiempo
        lastPosition = currentPosition;
        lastCheckTime = currentTime;

        // Verificar si la distancia es suficiente
        return distanceMoved >= minDistance;
    }
}
