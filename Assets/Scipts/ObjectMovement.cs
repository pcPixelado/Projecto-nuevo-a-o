using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public int maxAnts = 5; // M�ximo n�mero de hormigas que pueden mover el objeto
    public float speedMultiplier = 1.5f; // Incremento de velocidad por cada hormiga adicional
    public float baseSpeed = 1f; // Velocidad m�nima del objeto
    public float maxSpeed = 10f; // Velocidad m�xima que puede alcanzar el objeto

    private int currentAnts = 0; // Cantidad actual de hormigas empujando
    private Vector3 directionToMove; // Direcci�n hacia la que las hormigas est�n empujando
    private float currentSpeed = 0f; // Velocidad actual del objeto

    void Update()
    {
        if (currentAnts > 0)
        {
            // Mover el objeto en la direcci�n acumulada, usando la velocidad calculada
            transform.position += directionToMove * currentSpeed * Time.deltaTime;
        }
    }

    // A�adir una hormiga para que empuje el objeto
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

    // Calcular la velocidad del objeto en funci�n del n�mero de hormigas
    private void UpdateSpeedAndDirection()
    {
        // Actualizar la velocidad en funci�n del n�mero de hormigas
        currentSpeed = Mathf.Clamp(baseSpeed + (currentAnts - 1) * speedMultiplier, baseSpeed, maxSpeed);

        // Calcular la direcci�n hacia la que empujan las hormigas (simplificado: hacia su posici�n)
        if (currentAnts > 0)
        {
            directionToMove = (transform.position - GetAverageAntPosition()).normalized;
        }
    }

    // Calcular la velocidad de las hormigas (cuanto m�s hormigas, m�s r�pido)
    private float CalculateAntSpeed()
    {
        return Mathf.Clamp(baseSpeed + (currentAnts * speedMultiplier), baseSpeed, maxSpeed);
    }

    // Obtener la posici�n promedio de las hormigas que est�n empujando el objeto
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
