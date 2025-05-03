using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RemoveUIImageOnTrigger : MonoBehaviour
{
    [Tooltip("El objeto que debe tocar el trigger (ej: el jugador)")]
    public GameObject objeto1;

    [Tooltip("Lista de imágenes en el Canvas (orden acumulable)")]
    public List<GameObject> imagenes;

    [Tooltip("Panel que se mostrará cuando se acaben las imágenes")]
    public GameObject panelFinal;

    [Tooltip("Destino al que se teletransportará este objeto (objeto 2)")]
    public Transform puntoTeletransporte;

    private int imagenesRestantes;
    private bool puedeRecibirDaño = true;

    private void Start()
    {
        if (panelFinal != null)
            panelFinal.SetActive(false);

        imagenesRestantes = imagenes.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == objeto1 && puedeRecibirDaño)
        {
            StartCoroutine(RecibirDaño());
        }
    }

    IEnumerator RecibirDaño()
    {
        puedeRecibirDaño = false;

        if (imagenesRestantes > 0)
        {
            imagenesRestantes--;
            imagenes[imagenesRestantes].SetActive(false);

            if (imagenesRestantes == 0)
            {
                if (panelFinal != null)
                    panelFinal.SetActive(true);

                StartCoroutine(RestaurarTodoTrasEspera(4f));
            }
        }

        yield return new WaitForSeconds(1f); // Tiempo de invulnerabilidad

        puedeRecibirDaño = true;
    }

    IEnumerator RestaurarTodoTrasEspera(float segundos)
    {
        yield return new WaitForSeconds(segundos);

        // Teletransportar este objeto (objeto 2)
        if (puntoTeletransporte != null)
            transform.position = puntoTeletransporte.position;

        // Reactivar todas las imágenes
        foreach (GameObject img in imagenes)
            img.SetActive(true);

        imagenesRestantes = imagenes.Count;

        // Ocultar el panel
        if (panelFinal != null)
            panelFinal.SetActive(false);
    }
}
