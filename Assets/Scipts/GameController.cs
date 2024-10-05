using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{
    public int totalAnts = 10; // Total de hormigas en la escena
    private int remainingAnts; // Hormigas restantes que no han sido eliminadas o volcadas
    public float timeLimit = 60f; // Tiempo límite en segundos para cambiar de escena
    private float currentTime; // Tiempo transcurrido
    public string nextSceneName = "NextScene"; // Nombre de la escena a la que queremos ir
    private bool allAntsDefeated = false;

    private bool canSpawnObject = true; // Control del cooldown del lanzamiento de objetos
    public float objectSpawnCooldown = 3f; // Cooldown para lanzar objetos

    void Start()
    {
        remainingAnts = totalAnts; // Inicia con el número total de hormigas
        currentTime = 0f; // Inicializa el temporizador a 0
    }

    void Update()
    {
        currentTime += Time.deltaTime; // Incrementa el tiempo

        // Verifica si todas las hormigas han sido eliminadas o volcadas
        if (remainingAnts <= 0 && !allAntsDefeated)
        {
            allAntsDefeated = true;
            StartCoroutine(LoadSceneAfterDelay(2f)); // Cambiar de escena después de un retraso de 2 segundos
        }

        // Verificar si el tiempo límite ha pasado
        if (currentTime >= timeLimit)
        {
            SceneManager.LoadScene(nextSceneName); // Cambia de escena si el tiempo límite se ha cumplido
        }

        // Manejar el lanzamiento de objetos con cooldown
        HandleObjectSpawning();
    }

    // Método para reducir el número de hormigas restantes cuando son eliminadas o volcadas
    public void AntDefeated()
    {
        remainingAnts--;
    }

    // Método que cambia de escena después de un tiempo de espera
    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextSceneName);
    }

    // Manejar el lanzamiento de objetos con cooldown
    void HandleObjectSpawning()
    {
        if (Input.GetMouseButtonDown(0) && canSpawnObject) // Si el click izquierdo está presionado y no está en cooldown
        {
            // Aquí iría tu lógica para spawnear objetos
            SpawnObject();

            // Iniciar el cooldown
            StartCoroutine(ObjectSpawnCooldown());
        }
    }

    // Ejemplo de método para spawnear el objeto
    void SpawnObject()
    {
        Debug.Log("Objeto spawneado"); // Aquí agregas tu lógica de spawn
    }

    // Control del cooldown para el lanzamiento de objetos
    IEnumerator ObjectSpawnCooldown()
    {
        canSpawnObject = false; // No permitir que se spawneen más objetos
        yield return new WaitForSeconds(objectSpawnCooldown); // Esperar el tiempo del cooldown
        canSpawnObject = true; // Permitir el lanzamiento de objetos nuevamente
    }
}
