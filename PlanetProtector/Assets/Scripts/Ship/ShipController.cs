using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    // Mulitpliers for the ship's movement
    [SerializeField] private float speedMultiplier = 1f;
    [SerializeField] private float speedMultiplierAngle = 0.5f;
    [SerializeField] private float speedRollMultiplierAngle = 0.05f;

    private float verticalMove;
    private float horizontalMove;
    private float mouseInputX;
    private float mouseInputY;
    private float rollInput;

    private bool takeMouseInput;

    Rigidbody rb;
    float timer = 1f;
    [SerializeField] private Transform customCursorVisual; // Custom Cursor

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get rigidbody

        // Make the cursor invisible and confine it to the game window
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        takeMouseInput = true;
    }

    private void Update()
    {
        // Set the custom cursor to the position of the mouse position
        customCursorVisual.position = Input.mousePosition;

        // Timer goes down
        timer -= Time.deltaTime;

        verticalMove = Input.GetAxis("Vertical"); // A & D
        horizontalMove = Input.GetAxis("Horizontal"); // W & S
        rollInput = Input.GetAxis("Roll"); // E & Q

        if (takeMouseInput && timer <= 0f) // Check if the ship can use mouse input & To prevent the space ship from spinning in the beginning, wait a moment until    
        {
            mouseInputX = Input.GetAxis("Mouse X"); // Mouse X position
            mouseInputY = Input.GetAxis("Mouse Y"); // Mouse Y position
        }
        
        if (Input.GetKeyDown(KeyCode.X)) // Can it use mouse input or not
        {
            takeMouseInput = !takeMouseInput;
        }
    }

    // Add force to the ship, so it can move
    private void FixedUpdate()
    {
        if (!GameManager.Instance.gamePaused && !GameManager.Instance.gameEnded) AddForceToShip();
    }

    private void AddForceToShip()
    {
        // Forwards/Backwards & Left/Right
        rb.AddForce(rb.transform.TransformDirection(Vector3.forward) * verticalMove * speedMultiplier, ForceMode.VelocityChange);
        rb.AddForce(rb.transform.TransformDirection(Vector3.right) * horizontalMove * speedMultiplier, ForceMode.VelocityChange);

        // Mouse Position
        rb.AddTorque(rb.transform.right * speedMultiplierAngle * mouseInputY * -1, ForceMode.VelocityChange);
        rb.AddTorque(rb.transform.up * speedMultiplierAngle * mouseInputX, ForceMode.VelocityChange);

        // Rolling
        rb.AddTorque(rb.transform.forward * speedRollMultiplierAngle * rollInput, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyProjectile"))
        {
            Destroy(collision.gameObject);
            ShipDeath();
        }
    }

    public void ShipDeath()
    {
        Destroy(gameObject);
        GameManager.Instance.gameEnded = true;
        GameManager.Instance.gamePaused = true;
    }
}
