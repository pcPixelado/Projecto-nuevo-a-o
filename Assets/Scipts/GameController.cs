using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{
    public int totalAnts = 10;
    private int remainingAnts;
    public float timeLimit = 60f;
    private float currentTime;
    public string nextSceneName = "MenuCambiado";
    private bool allAntsDefeated = false;

    private bool canSpawnObject = true;
    public float objectSpawnCooldown = 3f;

    void Start()
    {
        remainingAnts = totalAnts;
        currentTime = 0f;
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (remainingAnts <= 0 && !allAntsDefeated)
        {
            allAntsDefeated = true;
            StartCoroutine(LoadSceneAfterDelay(2f));
        }

        if (currentTime >= timeLimit)
        {
            SceneManager.LoadScene(nextSceneName);
        }

        HandleObjectSpawning();
    }

    public void AntDefeated()
    {
        remainingAnts--;
    }

    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextSceneName);
    }

    void HandleObjectSpawning()
    {
        if (Input.GetMouseButtonDown(0) && canSpawnObject)
        {
            SpawnObject();
            StartCoroutine(ObjectSpawnCooldown());
        }
    }

    void SpawnObject()
    {
        Debug.Log("Objeto spawneado");
    }

    IEnumerator ObjectSpawnCooldown()
    {
        canSpawnObject = false;
        yield return new WaitForSeconds(objectSpawnCooldown);
        canSpawnObject = true;
    }
}
