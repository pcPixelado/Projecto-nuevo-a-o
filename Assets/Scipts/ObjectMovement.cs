using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public int maxAnts = 5; // Máximo número de hormigas que pueden mover el objeto
    public float speedMultiplier = 1.5f; // Incremento de velocidad por cada hormiga adicional
    public float baseSpeed = 1f; // Velocidad mínima del objeto
    public float maxSpeed = 10f; // Velocidad máxima que puede alcanzar el objeto

    private int currentAnts = 0; // Cantidad actual de hormigas empujando
    private Vector3 directionToMove; // Dirección hacia la que las hormigas están empujando
    private float currentSpeed = 0f; // Velocidad actual del objeto

    void Update()
    {
        if (currentAnts > 0)
        {
            // Mover el objeto en la dirección acumulada, usando la velocidad calculada
            transform.position += directionToMove * currentSpeed * Time.deltaTime;
        }
    }

    // Añadir una hormiga para que empuje el objeto
    public void AddAnt(AntMovement ant)
    {
        if (currentAnts < maxAnts)
        {
            currentAnts++;
            UpdateSpeedAndDirection();
            ant.AdjustSpeed(CalculateAntSpeed());
        }
    }

    // Remover una hormiga del objeto cuando deja de empujar
    public void RemoveAnt(AntMovement ant)
    {
        if (currentAnts > 0)
        {
            currentAnts--;
            UpdateSpeedAndDirection();
            ant.AdjustSpeed(baseSpeed);
        }
    }

    // Calcular la velocidad del objeto en función del número de hormigas
    private void UpdateSpeedAndDirection()
    {
        // Actualizar la velocidad en función del número de hormigas
        currentSpeed = Mathf.Clamp(baseSpeed + (currentAnts - 1) * speedMultiplier, baseSpeed, maxSpeed);

        // Calcular la dirección hacia la que empujan las hormigas (simplificado: hacia su posición)
        if (currentAnts > 0)
        {
            directionToMove = (transform.position - GetAverageAntPosition()).normalized;
        }
    }

    // Calcular la velocidad de las hormigas (cuanto más hormigas, más rápido)
    private float CalculateAntSpeed()
    {
        return Mathf.Clamp(baseSpeed + (currentAnts * speedMultiplier), baseSpeed, maxSpeed);
    }

    // Obtener la posición promedio de las hormigas que están empujando el objeto
    private Vector3 GetAverageAntPosition()
    {
        AntMovement[] ants = FindObjectsOfType<AntMovement>();
        Vector3 averagePosition = Vector3.zero;
        int antCount = 0;

        foreach (AntMovement ant in ants)
        {
            if (ant != null && ant.targetObject == gameObject)
            {
                averagePosition += ant.transform.position;
                antCount++;
            }
        }

        if (antCount > 0)
        {
            averagePosition /= antCount;
        }

        return averagePosition;
    }
}
