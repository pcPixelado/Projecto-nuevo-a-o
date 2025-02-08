using System.Collections;
using UnityEngine;

public class RaycastMovement : MonoBehaviour
{
    public float moveUpDistance = 2f; // Cuánto sube
    public float moveDownSpeed = 2f; // Velocidad al bajar
    public float raycastDistance = 0.1f; // Distancia del raycast hacia abajo
    public float waitTime = 1f; // Tiempo de espera antes de subir
    public LayerMask groundLayer; // Capa del suelo

    private Vector3 startPosition;
    private bool goingUp = true;

    void Start()
    {
        startPosition = transform.position;
        StartCoroutine(MoveCycle());
    }

    IEnumerator MoveCycle()
    {
        while (true)
        {
            if (goingUp)
            {
                // Subir hasta la distancia indicada
                Vector3 targetPosition = startPosition + Vector3.up * moveUpDistance;
                while (transform.position.y < targetPosition.y)
                {
                    transform.position += Vector3.up * moveDownSpeed * Time.deltaTime;
                    yield return null;
                }
                goingUp = false;
            }
            else
            {
                // Bajar lentamente hasta que el raycast detecte algo
                while (!Physics.Raycast(transform.position, Vector3.down, raycastDistance, groundLayer))
                {
                    transform.position -= Vector3.up * moveDownSpeed * Time.deltaTime;
                    yield return null;
                }

                // Espera antes de volver a subir
                yield return new WaitForSeconds(waitTime);

                goingUp = true;
            }
        }
    }
}
