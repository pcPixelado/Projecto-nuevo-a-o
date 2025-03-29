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

    public void Update()
    {
        //SALTO
        OnFloor = Physics.CheckSphere(floorDetector.position, 0.1f, layerFloor);
        if (OnFloor == true && Input.GetButtonDown("Jump"))
        {
            rig.AddForce(Vector3.up * jumpForce);
        }
        //MOVIMIENTO
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * horizontalMove + transform.forward * verticalMove;

        rig.velocity = direction * speed;
    }
}