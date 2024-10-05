using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{
    public int totalAnts = 10; // Total de hormigas en la escena
    private int remainingAnts; // Hormigas restantes que no han sido eliminadas o volcadas
    public float timeLimit = 60f; // Tiempo l�mite en segundos para cambiar de escena
    private float currentTime; // Tiempo transcurrido
    public string nextSceneName = "NextScene"; // Nombre de la escena a la que queremos ir
    private bool allAntsDefeated = false;

    private bool canSpawnObject = true; // Control del cooldown del lanzamiento de objetos
    public float objectSpawnCooldown = 3f; // Cooldown para lanzar objetos

    void Start()
    {
        remainingAnts = totalAnts; // Inicia con el n�mero total de hormigas
        currentTime = 0f; // Inicializa el temporizador a 0
    }

    void Update()
    {
        currentTime += Time.deltaTime; // Incrementa el tiempo

        // Verifica si todas las hormigas han sido eliminadas o volcadas
        if (remainingAnts <= 0 && !allAntsDefeated)
        {
            allAntsDefeated = true;
            StartCoroutine(LoadSceneAfterDelay(2f)); // Cambiar de escena despu�s de un retraso de 2 segundos
        }

        // Verificar si el tiempo l�mite ha pasado
        if (currentTime >= timeLimit)
        {
            SceneManager.LoadScene(nextSceneName); // Cambia de escena si el tiempo l�mite se ha cumplido
        }

        // Manejar el lanzamiento de objetos con cooldown
        HandleObjectSpawning();
    }

    // M�todo para reducir el n�mero de hormigas restantes cuando son eliminadas o volcadas
    public void AntDefeated()
    {
        remainingAnts--;
    }

    // M�todo que cambia de escena despu�s de un tiempo de espera
    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextSceneName);
    }

    // Manejar el lanzamiento de objetos con cooldown
    void HandleObjectSpawning()
    {
        if (Input.GetMouseButtonDown(0) && canSpawnObject) // Si el click izquierdo est� presionado y no est� en cooldown
        {
            // Aqu� ir�a tu l�gica para spawnear objetos
            SpawnObject();

            // Iniciar el cooldown
            StartCoroutine(ObjectSpawnCooldown());
        }
    }

    // Ejemplo de m�todo para spawnear el objeto
    void SpawnObject()
    {
        Debug.Log("Objeto spawneado"); // Aqu� agregas tu l�gica de spawn
    }

    // Control del cooldown para el lanzamiento de objetos
    IEnumerator ObjectSpawnCooldown()
    {
        canSpawnObject = false; // No permitir que se spawneen m�s objetos
        yield return new WaitForSeconds(objectSpawnCooldown); // Esperar el tiempo del cooldown
        canSpawnObject = true; // Permitir el lanzamiento de objetos nuevamente
    }
}
