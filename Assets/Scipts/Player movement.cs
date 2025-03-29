using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed, jumpForce;
    Rigidbody rig;
    bool OnFloor;
    public Transform floorDetector;
    public LayerMask layerFloor;

    void Start()
    {
        rig = GetComponent<Rigidbody>(); // Inicializar el Rigidbody
    }

    public void Update()
    {
        // SALTO
        OnFloor = Physics.CheckSphere(floorDetector.position, 0.1f, layerFloor);
        if (OnFloor == true && Input.GetButtonDown("Jump"))
        {
            rig.velocity = new Vector3(rig.velocity.x, jumpForce, rig.velocity.z); // Usar velocity en lugar de AddForce
        }

        // MOVIMIENTO
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * horizontalMove + transform.forward * verticalMove;

        rig.velocity = new Vector3(direction.x * speed, rig.velocity.y, direction.z * speed); // Mantener la velocidad en Y
    }
}