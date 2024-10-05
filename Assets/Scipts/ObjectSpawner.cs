using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] spawnableObjects; // Array de objetos que se pueden spawnear (asignar desde el inspector)
    private GameObject selectedObject; // El objeto actualmente seleccionado para spawnear
    private Camera mainCamera; // Cámara principal

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandleObjectSelection(); // Maneja la selección de objetos con 1, 2 o 3
        HandleSpawning(); // Maneja el click y el spawn
    }

    // Maneja la selección de objetos con las teclas 1, 2 o 3
    void HandleObjectSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedObject = spawnableObjects[0]; // Selecciona el primer objeto
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedObject = spawnableObjects[1]; // Selecciona el segundo objeto
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedObject = spawnableObjects[2]; // Selecciona el tercer objeto
        }
    }

    // Maneja el spawneo del objeto con el click izquierdo del mouse
    void HandleSpawning()
    {
        if (Input.GetMouseButtonDown(0) && selectedObject != null) // Si haces click izquierdo y hay un objeto seleccionado
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 spawnPosition = hit.point + new Vector3(0, 5f, 0); // Spawnear el objeto un poco por encima del punto de click
                GameObject spawnedObject = Instantiate(selectedObject, spawnPosition, Quaternion.identity); // Instanciar el objeto
                spawnedObject.AddComponent<Rigidbody>(); // Añadir Rigidbody para que el objeto caiga
                spawnedObject.AddComponent<SpawnedObject>(); // Añadir el script de comportamiento del objeto spawneado
            }
        }
    }
}
