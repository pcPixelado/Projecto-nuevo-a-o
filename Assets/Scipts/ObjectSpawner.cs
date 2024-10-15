using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    private GameObject grabbedObject = null;
    private Rigidbody grabbedRigidbody = null;

    public float grabDistance = 5f;
    public float throwForce = 10f;
    public float holdDistance = 2f;
    public float smoothTime = 0.1f;

    public float moveSpeed = 5f;
    public float crouchSpeed = 2.5f;
    private Vector3 holdPositionVelocity;

    private CharacterController controller;
    private bool isCrouching = false;

    private float originalHeight;
    public float crouchHeight = 1f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
    }

    void Update()
    {
        HandleObjectGrab();
        HandlePlayerMovement();
        HandleCrouch();
    }

    void HandleObjectGrab()
    {
        if (grabbedObject == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                TryGrabObject();
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                ThrowObject();
            }
            else
            {
                HoldObject();
            }
        }
    }

    void TryGrabObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, grabDistance))
        {
            if (hit.collider.gameObject.GetComponent<Rigidbody>() != null)
            {
                grabbedObject = hit.collider.gameObject;
                grabbedRigidbody = grabbedObject.GetComponent<Rigidbody>();

                grabbedRigidbody.useGravity = false;
                grabbedRigidbody.velocity = Vector3.zero;
            }
        }
    }

    void HoldObject()
    {
        Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * holdDistance;
        grabbedRigidbody.MovePosition(Vector3.SmoothDamp(grabbedObject.transform.position, targetPosition, ref holdPositionVelocity, smoothTime));

        grabbedObject.transform.rotation = Quaternion.identity;
    }

    void ThrowObject()
    {
        grabbedRigidbody.useGravity = true;
        grabbedRigidbody.AddForce(Camera.main.transform.forward * throwForce, ForceMode.VelocityChange);

        grabbedObject = null;
        grabbedRigidbody = null;
    }

    void HandlePlayerMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        float speed = isCrouching ? crouchSpeed : moveSpeed;
        controller.Move(move * speed * Time.deltaTime);
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
            controller.height = isCrouching ? crouchHeight : originalHeight;
        }
    }
}
