using UnityEngine;

public class SpawnedObject : MonoBehaviour
{
    private bool hasTouchedGround = false; // Saber si ha tocado el suelo

    void OnCollisionEnter(Collision collision)
    {
        // Verificar si toca un objeto con el tag "Suelo"
        if (collision.gameObject.CompareTag("Suelo") && !hasTouchedGround)
        {
            hasTouchedGround = true; // Marcar que ya tocó el suelo
            StartCoroutine(DestroyAfterDelay()); // Iniciar la espera de 2 segundos antes de destruir el objeto
        }

        // Verificar si toca un objeto con el tag "Ant" (hormiga)
        if (collision.gameObject.CompareTag("Ant"))
        {
            AntMovement antMovement = collision.gameObject.GetComponent<AntMovement>();
            if (antMovement != null)
            {
                antMovement.KnockOver(); // Llamar al método para voltear la hormiga
            }
        }
    }

    // Espera 2 segundos y destruye el objeto
    System.Collections.IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(0.3f); // Esperar 2 segundos
        Destroy(gameObject); // Destruir el objeto
    }
}
