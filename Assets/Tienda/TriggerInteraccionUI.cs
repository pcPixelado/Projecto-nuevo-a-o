using UnityEngine;

public class TriggerInteraccionUI : MonoBehaviour
{
    [Tooltip("El objeto jugador que debe tocar el trigger")]
    public GameObject objetoJugador;

    [Tooltip("Texto que se muestra al entrar al trigger")]
    public GameObject textoInteractuar;

    [Tooltip("Panel que se abre/cierra al pulsar E")]
    public GameObject panelInteraccion;

    private bool dentroDelTrigger = false;

    void Start()
    {
        if (textoInteractuar != null)
            textoInteractuar.SetActive(false);

        if (panelInteraccion != null)
            panelInteraccion.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == objetoJugador)
        {
            dentroDelTrigger = true;
            if (textoInteractuar != null)
                textoInteractuar.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == objetoJugador)
        {
            dentroDelTrigger = false;
            if (textoInteractuar != null)
                textoInteractuar.SetActive(false);

            // Opcional: cerrar el panel automáticamente al salir
            if (panelInteraccion != null)
                panelInteraccion.SetActive(false);
        }
    }

    void Update()
    {
        if (dentroDelTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (panelInteraccion != null)
                panelInteraccion.SetActive(!panelInteraccion.activeSelf); // Toggle
        }
    }
}
