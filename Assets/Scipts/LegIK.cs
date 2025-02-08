using UnityEngine;

public class LegIK : MonoBehaviour
{
    public Transform footTarget; // El objeto que representará la posición final de la pata
    public LayerMask groundLayer; // La capa del suelo
    public float raycastDistance = 2f; // Distancia máxima del raycast
    public float footOffset = 0.1f; // Pequeño ajuste para que no se hunda en el suelo

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, groundLayer))
        {
            footTarget.position = hit.point + (Vector3.up * footOffset); // Fijar la pata al suelo
        }
    }
}
