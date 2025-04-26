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
        rig = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor al centro de la pantalla
        Cursor.visible = false; // Oculta el cursor
    }

    void Update()
{
    // SALTO
    OnFloor = Physics.CheckSphere(floorDetector.position, 0.1f, layerFloor);
    if (OnFloor && Input.GetButtonDown("Jump"))
    {
        rig.velocity = new Vector3(rig.velocity.x, jumpForce, rig.velocity.z);
    }

    // MOVIMIENTO
    float horizontalMove = Input.GetAxis("Horizontal");
    float verticalMove = Input.GetAxis("Vertical");

    Vector3 direction = transform.right * horizontalMove + transform.forward * verticalMove;
    
    // Atención aquí: conservamos la velocidad Y original
    Vector3 moveVelocity = new Vector3(direction.x * speed, rig.velocity.y, direction.z * speed);
    rig.velocity = moveVelocity;
}

}