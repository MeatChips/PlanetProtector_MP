using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 1f;
    [SerializeField] private float speedMultiplierAngle = 0.5f;
    [SerializeField] private float speedRollMultiplierAngle = 0.05f;

    private float verticalMove;
    private float horizontalMove;
    private float mouseInputX;
    private float mouseInputY;
    private float rollInput;

    Rigidbody rb;
    float timer = 1f;
    [SerializeField] private Transform customCursorVisual; // Custom Cursor

    //Quaternion originalRotation;

    private void Awake()
    {
        //originalRotation.x = transform.rotation.x;
        //originalRotation.z = transform.rotation.z;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get rigidbody

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update()
    {
        customCursorVisual.position = Input.mousePosition;

        timer -= Time.deltaTime;

        verticalMove = Input.GetAxis("Vertical");
        horizontalMove = Input.GetAxis("Horizontal");
        rollInput = Input.GetAxis("Roll");

        if (timer <= 0f)
        {
            mouseInputX = Input.GetAxis("Mouse X");
            mouseInputY = Input.GetAxis("Mouse Y");
        }
    }

    private void FixedUpdate()
    {
        AddForceToShip();

        //if(Input.GetKey(KeyCode.X)) 
        //{
        //    gameObject.transform.rotation = originalRotation;
        //}
    }

    private void AddForceToShip()
    {
        rb.AddForce(rb.transform.TransformDirection(Vector3.forward) * verticalMove * speedMultiplier, ForceMode.VelocityChange);
        rb.AddForce(rb.transform.TransformDirection(Vector3.right) * horizontalMove * speedMultiplier, ForceMode.VelocityChange);

        rb.AddTorque(rb.transform.right * speedMultiplierAngle * mouseInputY * -1, ForceMode.VelocityChange);
        rb.AddTorque(rb.transform.up * speedMultiplierAngle * mouseInputX, ForceMode.VelocityChange);

        rb.AddTorque(rb.transform.forward * speedRollMultiplierAngle * rollInput, ForceMode.VelocityChange);
    }
}

/*
W = forward
S = backward
A = leftward
D = rightward

Spacebar = thrust up
C = thrust down

Q = roll left(anti-clockwise)
E = roll right(clockwise)

Cursor = always rotate towards
 */
