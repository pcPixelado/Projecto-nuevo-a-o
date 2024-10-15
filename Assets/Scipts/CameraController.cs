using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerBody; // Referencia al transform del jugador
    public Vector3 offset; // Offset de la c�mara respecto al jugador
    public float mouseSensitivity = 100f; // Sensibilidad del rat�n

    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor en el centro
    }

    void Update()
    {
        HandleMouseLook();
        FollowPlayer();
    }

    // Controla la rotaci�n de la c�mara con el movimiento del rat�n
    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; // Controla la rotaci�n vertical (mirar arriba/abajo)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limita la rotaci�n vertical

        yRotation += mouseX; // Controla la rotaci�n horizontal (mirar izquierda/derecha)

        // Aplica la rotaci�n a la c�mara (vertical) y al jugador (horizontal)
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    // Hace que la c�mara siga al jugador con un offset
    void FollowPlayer()
    {
        transform.position = playerBody.position + offset; // Suma el offset para seguir al jugador
    }
}
