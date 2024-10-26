using UnityEngine;

public class RotarObjeto : MonoBehaviour
{
    // Velocidad de rotación en grados por segundo
    public float velocidadRotacion = 50f;

    void Update()
    {
        // Rotar el objeto en el eje Y hacia la derecha
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
    }
}
