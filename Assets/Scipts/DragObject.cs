using UnityEngine;


public class DragObject : MonoBehaviour
{
    private Camera mainCamera; // La cámara principal
    private GameObject selectedObject; // El objeto actualmente seleccionado para arrastrar
    public string targetTag = "Target"; // El tag del objeto que puedes arrastrar
    private Vector3 offset; // Diferencia entre la posición del mouse y el objeto al arrastrarlo

    void Start()
    {
        mainCamera = Camera.main; // Obtener la cámara principal
    }

    void Update()
    {
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0)) // Al hacer click izquierdo
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // Lanzar un ray desde la cámara hasta el mouse

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag(targetTag)) // Verificar si hemos hecho click sobre un objeto con el tag "Target"
                {
                    selectedObject = hit.collider.gameObject; // Guardar el objeto seleccionado
                    offset = selectedObject.transform.position - GetMouseWorldPosition(); // Calcular la diferencia de posición para evitar "saltos"
                }
            }
        }

        if (Input.GetMouseButton(0) && selectedObject != null) // Mientras mantienes el click
        {
            selectedObject.transform.position = GetMouseWorldPosition() + offset; // Mover el objeto siguiendo el mouse
        }

        if (Input.GetMouseButtonUp(0)) // Soltar el objeto al soltar el click
        {
            selectedObject = null; // Desactivar la selección del objeto
        }
    }

    // Obtener la posición del mouse en el mundo 3D
    Vector3 GetMouseWorldPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero); // Crear un plano en el eje Y (plano horizontal)
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance); // Obtener el punto donde el ray intersecta el plano
        }
        return Vector3.zero;
    }
}
