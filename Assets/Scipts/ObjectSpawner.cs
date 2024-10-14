using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] spawnableObjects; // Array de objetos que se pueden spawnear (asignar desde el inspector)
    private GameObject selectedObject; // El objeto actualmente seleccionado para spawnear
    private Camera mainCamera; // C�mara principal

    public float mouseSensitivity = 100f; // Sensibilidad del rat�n para rotaci�n
    public float smoothTime = 0.1f; // Tiempo para suavizar la rotaci�n

    private float xRotation = 0f; // Control de la rotaci�n en el eje X (vertical)
    private float yRotation = 0f; // Control de la rotaci�n en el eje Y (horizontal)
    private Vector2 currentRotation; // Rotaci�n actual para suavizar
    private Vector2 currentRotationVelocity; // Velocidad para la interpolaci�n suave

    void Start()
    {
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor en el centro de la pantalla
    }

    void Update()
    {
        HandleObjectSelection(); // Maneja la selecci�n de objetos con 1, 2 o 3
        HandleSpawning(); // Maneja el click y el spawn
        HandleMouseLook(); // Controla la rotaci�n de la c�mara con el rat�n
    }

    // Maneja la selecci�n de objetos con las teclas 1, 2 o 3
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

    // Maneja el spawneo del objeto en el centro de la c�mara
    void HandleSpawning()
    {
        if (Input.GetMouseButtonDown(0) && selectedObject != null) // Si haces click izquierdo y hay un objeto seleccionado
        {
            Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * 20f; // Spawnea 10 unidades frente a la c�mara
            GameObject spawnedObject = Instantiate(selectedObject, spawnPosition, Quaternion.identity); // Instanciar el objeto
            spawnedObject.AddComponent<Rigidbody>(); // A�adir Rigidbody para que el objeto caiga
            spawnedObject.AddComponent<SpawnedObject>(); // A�adir el script de comportamiento del objeto spawneado
        }
    }

    // Controla la rotaci�n de la c�mara con el movimiento del rat�n
    void HandleMouseLook()
    {
        // Capturar movimiento del rat�n
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Actualizar rotaciones horizontal y vertical
        yRotation += mouseX; // Rotaci�n en el eje Y (horizontal)
        xRotation -= mouseY; // Rotaci�n en el eje X (vertical)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limitar rotaci�n vertical entre -90 y 90 grados

        // Suavizar la rotaci�n
        currentRotation = Vector2.SmoothDamp(currentRotation, new Vector2(xRotation, yRotation), ref currentRotationVelocity, smoothTime);

        // Aplicar rotaci�n a la c�mara
        mainCamera.transform.localRotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0f);
    }
}

