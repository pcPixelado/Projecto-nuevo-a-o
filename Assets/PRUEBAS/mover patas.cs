using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoverObjetosControlado : MonoBehaviour
{
    [Header("Objeto principal (movimiento hacia adelante)")]
    public Transform objetoPrincipal;
    public float velocidadMovimiento = 5f;

    [Header("Objetos secundarios (movimiento controlado)")]
    public List<Transform> objetosSecundarios; // Lista de objetos secundarios
    public float alturaSubida = 1f; // Altura a la que suben después de tocar el suelo
    public float velocidadMovimientoVertical = 2f; // Velocidad vertical
    public float retrasoEntreMovimientos = 1f; // Tiempo de espera entre movimientos

    [Header("Detección del suelo")]
    public string tagSuelo = "Suelo";
    public float distanciaRaycast = 0.2f; // Alcance del raycast hacia abajo

    private Dictionary<Transform, Vector3> posicionesIniciales = new Dictionary<Transform, Vector3>();
    private Dictionary<Transform, bool> enMovimiento = new Dictionary<Transform, bool>(); // Control del estado de movimiento

    void Start()
    {
        // Inicializar las posiciones iniciales y estados
        foreach (Transform objeto in objetosSecundarios)
        {
            posicionesIniciales[objeto] = objeto.position;
            enMovimiento[objeto] = false;
            StartCoroutine(ControlarMovimiento(objeto)); // Iniciar el ciclo de movimiento
        }
    }

    void Update()
    {
        // Movimiento del objeto principal hacia adelante
        if (objetoPrincipal != null)
        {
            objetoPrincipal.Translate(Vector3.forward * velocidadMovimiento * Time.deltaTime);
        }
    }

    IEnumerator ControlarMovimiento(Transform objetoSecundario)
    {
        while (true)
        {
            // Comenzar con el movimiento hacia abajo
            yield return StartCoroutine(MoverVerticalmente(objetoSecundario, -1));

            // Subir después de tocar el suelo
            yield return StartCoroutine(MoverVerticalmente(objetoSecundario, 1));

            // Esperar antes de repetir el ciclo
            yield return new WaitForSeconds(retrasoEntreMovimientos);
        }
    }

    IEnumerator MoverVerticalmente(Transform objetoSecundario, int direccion)
    {
        enMovimiento[objetoSecundario] = true;
        Vector3 posicionObjetivo = objetoSecundario.position;

        if (direccion == -1) // Moviendo hacia abajo
        {
            // Bajar hasta tocar el suelo
            while (!DetectarSuelo(objetoSecundario))
            {
                posicionObjetivo.y -= velocidadMovimientoVertical * Time.deltaTime;
                objetoSecundario.position = posicionObjetivo;
                yield return null; // Esperar al siguiente frame
            }
        }
        else if (direccion == 1) // Moviendo hacia arriba
        {
            // Subir hasta la altura definida
            float alturaMaxima = posicionesIniciales[objetoSecundario].y + alturaSubida;
            while (objetoSecundario.position.y < alturaMaxima)
            {
                posicionObjetivo.y += velocidadMovimientoVertical * Time.deltaTime;
                objetoSecundario.position = posicionObjetivo;
                yield return null; // Esperar al siguiente frame
            }
        }

        enMovimiento[objetoSecundario] = false;
    }

    bool DetectarSuelo(Transform objetoSecundario)
    {
        // Crear el raycast desde la posición del objeto secundario hacia abajo
        Ray ray = new Ray(objetoSecundario.position, Vector3.down);
        RaycastHit hit;

        // Verificar si el raycast colisiona con un objeto con el tag "Suelo"
        if (Physics.Raycast(ray, out hit, distanciaRaycast))
        {
            if (hit.collider.CompareTag(tagSuelo))
            {
                return true;
            }
        }

        return false;
    }

    void OnDrawGizmos()
    {
        // Dibujar raycast para cada objeto secundario
        Gizmos.color = Color.red;
        foreach (Transform objeto in objetosSecundarios)
        {
            if (objeto != null)
            {
                Gizmos.DrawLine(objeto.position, objeto.position + Vector3.down * distanciaRaycast);
            }
        }
    }
}

