using System.Collections;
using UnityEngine;

public class RaycastMovement : MonoBehaviour
{
    public float moveUpAmount = 2f; // Cuánto sube al tocar el suelo
    public float moveDownSpeed = 2f; // Velocidad al bajar
    public float raycastDistance = 0.1f; // Distancia del raycast hacia abajo
    public float waitTime = 1f; // Tiempo de espera antes de subir
    public LayerMask groundLayer; // Capa del suelo

    private bool goingUp = false;

    void Start()
    {
        StartCoroutine(MoveCycle());
    }

    IEnumerator MoveCycle()
    {
        while (true)
        {
            // Bajar lentamente hasta que el raycast detecte algo
            while (!Physics.Raycast(transform.position, Vector3.down, raycastDistance, groundLayer))
            {
                transform.position -= Vector3.up * moveDownSpeed * Time.deltaTime;
                yield return null;
            }

            // Espera antes de volver a subir
            yield return new WaitForSeconds(waitTime);

            // Sube solo 2 unidades desde la posición actual
            Vector3 targetPosition = transform.position + Vector3.up * moveUpAmount;
            while (transform.position.y < targetPosition.y)
            {
                transform.position += Vector3.up * moveDownSpeed * Time.deltaTime;
                yield return null;
            }
        }
    }
}
