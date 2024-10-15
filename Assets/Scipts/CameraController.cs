using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerBody; // Referencia al transform del jugador
    public Vector3 offset; // Offset de la cámara respecto al jugador
    public float mouseSensitivity = 100f; // Sensibilidad del ratón

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

    // Controla la rotación de la cámara con el movimiento del ratón
    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; // Controla la rotación vertical (mirar arriba/abajo)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limita la rotación vertical

        yRotation += mouseX; // Controla la rotación horizontal (mirar izquierda/derecha)

        // Aplica la rotación a la cámara (vertical) y al jugador (horizontal)
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    // Hace que la cámara siga al jugador con un offset
    void FollowPlayer()
    {
        transform.position = playerBody.position + offset; // Suma el offset para seguir al jugador
    }
}
