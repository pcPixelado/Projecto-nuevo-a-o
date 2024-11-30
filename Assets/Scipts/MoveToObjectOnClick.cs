using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement; // Asegúrate de incluir esto

public class AntMovement : MonoBehaviour
{
    public string targetTag = "Target";
    public GameObject baseDestination;
    public float baseSpeed = 3.5f;
    public float maxSpeed = 10f;
    private NavMeshAgent agent;
    public GameObject targetObject;
    private ObjectMovement objectMovementScript;
    private bool isCarryingObject = false;
    private Animator animator;
    private bool isKnockedOver = false;

    private GameController gameController;

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
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f);
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.CompareTag(targetTag))
                    {
                        targetObject = hitCollider.gameObject;
                        objectMovementScript = targetObject.GetComponent<ObjectMovement>();

                        if (objectMovementScript != null)
                        {
                            objectMovementScript.AddAnt(this);
                        }
                        break;
                    }
                }
            }

            if (targetObject != null)
            {
                agent.SetDestination(targetObject.transform.position);

                if (Vector3.Distance(transform.position, targetObject.transform.position) < 1f)
                {
                    isCarryingObject = true;
                }
            }
        }
        else
        {
            if (baseDestination != null)
            {
                agent.SetDestination(baseDestination.transform.position);

                if (targetObject != null)
                {
                    targetObject.transform.position = transform.position;
                }

                if (Vector3.Distance(transform.position, baseDestination.transform.position) < 1f)
                {
                    isCarryingObject = false;

                    if (objectMovementScript != null)
                    {
                        objectMovementScript.RemoveAnt(this);
                    }
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

            transform.Rotate(180f, 0f, 0f);

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
            isKnockedOver = true;
        }
        if (other.CompareTag("Target"))
        {
            Debug.Log("Colisión con el objeto objetivo. Cambiando de escena...");
            SceneManager.LoadScene("MenuNiveles"); // Usa el nombre de tu escena o `nextSceneName`
        }
    }
    
    
      
    
}
