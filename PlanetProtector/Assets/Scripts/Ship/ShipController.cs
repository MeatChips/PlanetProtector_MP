using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    // Mulitpliers for the ship's movement
    [SerializeField] private float speedForwardMultiplier = 1f;
    [SerializeField] private float speedBackwardMultiplier = .7f;
    [SerializeField] private float speedLeftRightMultiplier = .8f;
    [SerializeField] private float speedRollMultiplierAngle = 0.05f;
    [SerializeField] private GameObject explosionParticle;

    private float mouseInputX;
    private float mouseInputY;

    private bool takeMouseInput;

    Rigidbody rb;
    float timer = .1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get rigidbody

        takeMouseInput = true;
    }

    private void Update()
    {
        // Timer goes down
        timer -= Time.deltaTime;

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
        if (!GameManager.Instance.gamePaused && !GameManager.Instance.gameEnded && GameManager.Instance.gameStarted) AddForceToShip();
    }

    private void AddForceToShip()
    {
        // Forwards/Backwards & Left/Right
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(rb.transform.TransformDirection(Vector3.forward) * speedForwardMultiplier, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(rb.transform.TransformDirection(Vector3.back) * speedBackwardMultiplier, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(rb.transform.TransformDirection(Vector3.right) * speedLeftRightMultiplier, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(rb.transform.TransformDirection(Vector3.left) * speedLeftRightMultiplier, ForceMode.VelocityChange);
        }

        // Mouse Position -- / 1000f otherwise the number would be too big and the rotation of the ship would be too fast.
        rb.AddTorque(rb.transform.right * (GameManager.Instance.SensitivityY / 1000f) * mouseInputY * -1, ForceMode.VelocityChange);
        rb.AddTorque(rb.transform.up * (GameManager.Instance.SensitivityX / 1000f) * mouseInputX, ForceMode.VelocityChange);

        // Rolling
        if (Input.GetKey(KeyCode.Q))
        {
            rb.AddTorque(rb.transform.forward * speedRollMultiplierAngle, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.E))
        {
            rb.AddTorque(rb.transform.forward * -speedRollMultiplierAngle, ForceMode.VelocityChange);
        }
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
        GameObject explosionEffectGO = Instantiate(explosionParticle, transform.position, transform.rotation);
        Destroy(explosionEffectGO, 2f);
        Destroy(gameObject);
        GameManager.Instance.gameEnded = true;
        GameManager.Instance.gamePaused = true;
    }
}
