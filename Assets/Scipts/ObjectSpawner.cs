using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] spawnableObjects; // Array de objetos que se pueden spawnear (asignar desde el inspector)
    private GameObject selectedObject; // El objeto actualmente seleccionado para spawnear
    private Camera mainCamera; // Cámara principal

    public float mouseSensitivity = 100f; // Sensibilidad del ratón para rotación
    public float smoothTime = 0.1f; // Tiempo para suavizar la rotación

    private float xRotation = 0f; // Control de la rotación en el eje X (vertical)
    private float yRotation = 0f; // Control de la rotación en el eje Y (horizontal)
    private Vector2 currentRotation; // Rotación actual para suavizar
    private Vector2 currentRotationVelocity; // Velocidad para la interpolación suave

    void Start()
    {
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor en el centro de la pantalla
    }

    void Update()
    {
        HandleObjectSelection(); // Maneja la selección de objetos con 1, 2 o 3
        HandleSpawning(); // Maneja el click y el spawn
        HandleMouseLook(); // Controla la rotación de la cámara con el ratón
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

    // Maneja el spawneo del objeto en el centro de la cámara
    void HandleSpawning()
    {
        if (Input.GetMouseButtonDown(0) && selectedObject != null) // Si haces click izquierdo y hay un objeto seleccionado
        {
            Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * 20f; // Spawnea 10 unidades frente a la cámara
            GameObject spawnedObject = Instantiate(selectedObject, spawnPosition, Quaternion.identity); // Instanciar el objeto
            spawnedObject.AddComponent<Rigidbody>(); // Añadir Rigidbody para que el objeto caiga
            spawnedObject.AddComponent<SpawnedObject>(); // Añadir el script de comportamiento del objeto spawneado
        }
    }

    // Controla la rotación de la cámara con el movimiento del ratón
    void HandleMouseLook()
    {
        // Capturar movimiento del ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Actualizar rotaciones horizontal y vertical
        yRotation += mouseX; // Rotación en el eje Y (horizontal)
        xRotation -= mouseY; // Rotación en el eje X (vertical)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limitar rotación vertical entre -90 y 90 grados

        // Suavizar la rotación
        currentRotation = Vector2.SmoothDamp(currentRotation, new Vector2(xRotation, yRotation), ref currentRotationVelocity, smoothTime);

        // Aplicar rotación a la cámara
        mainCamera.transform.localRotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0f);
    }
}

