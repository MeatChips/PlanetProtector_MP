using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Transform target; // The target it has to fly too and shoot (the planet)
    [SerializeField] private float floatSpeed = 5f; // The ship its normal speed
    [SerializeField] private float fallSpeed = 50f; // The ship its charge speed
    private float speed;
    [SerializeField] private float sphereColRadius = 100f; // Radius of detection sphere
    private Rigidbody rb;
    private SphereCollider sphereCol;
    private PlanetHealthManager planetHealthManager;

    private Transform asteroidVisual;
    private float rotationAsteroidSpeed = 10f;

    private float health = 6f;
    private int killReward = 1;

    private bool canMove, detectedTarget;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCol = GetComponent<SphereCollider>();
        asteroidVisual = gameObject.transform.GetChild(0);

        if (!GameManager.Instance.gameEnded)
        {
            target = GameObject.FindGameObjectWithTag("Target").transform;
            planetHealthManager = GameObject.FindGameObjectWithTag("Target").GetComponent<PlanetHealthManager>();
        }

        sphereCol.radius = sphereColRadius;

        canMove = true;
        detectedTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !GameManager.Instance.gamePaused && !GameManager.Instance.gameEnded) MoveTowardsTarget();
        if (detectedTarget && !GameManager.Instance.gamePaused && !GameManager.Instance.gameEnded) ChargeTowardsPlanet();

        asteroidVisual.Rotate(Vector3.up * rotationAsteroidSpeed * Time.deltaTime);
        asteroidVisual.Rotate(Vector3.right * rotationAsteroidSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            canMove = false;
            detectedTarget = true;
        }
    }

    // Move ship
    private void MoveTowardsTarget()
    {
        speed = floatSpeed;
        transform.LookAt(target); // Look at the target (the planet)
        if (target != null)
        {
            rb.velocity = transform.forward * speed;
        }
    }

    private void ChargeTowardsPlanet()
    {
        speed = fallSpeed;
        transform.LookAt(target); // Look at the target (the planet)
        if (target != null)
        {
            rb.velocity = transform.forward * speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(this.gameObject);
            planetHealthManager.TakeDamage(5);
        }

        if (collision.gameObject.CompareTag("ShipProjectile"))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        health -= 1f;
        if (health <= 0)
        {
            GameManager.Instance.Score += killReward;
            Destroy(this.gameObject);
        }
    }
}
