using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : MonoBehaviour
{
    [SerializeField] private Transform target; // The target it has to fly too and shoot (the human base)
    [SerializeField] private GameObject[] targets; // Array of targets
    [SerializeField] private float thrustSpeed = 5f; // The ship its normal speed
    [SerializeField] private float chargeSpeed = 50f; // The ship its charge speed
    [SerializeField] private GameObject explosionParticle;
    private float speed;
    [SerializeField] private float sphereColRadius = 100f; // Radius of detection sphere
    private Rigidbody rb;
    private SphereCollider sphereCol;
    private PlanetHealthManager planetHealthManager;

    private float health = 4f;
    private int killReward = 8;

    private bool canMove, detectedTarget;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCol = GetComponent<SphereCollider>();

        if (!GameManager.Instance.gameEnded)
        {
            targets = GameObject.FindGameObjectsWithTag("Target");
            target = targets[Random.Range(0, targets.Length)].transform;
            /*            target = GameObject.FindGameObjectWithTag("Target").transform;*/
            planetHealthManager = GameObject.FindGameObjectWithTag("Target").transform.parent.GetComponent<PlanetHealthManager>();
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
        speed = thrustSpeed;
        transform.LookAt(target); // Look at the target (the planet)
        if (target != null)
        {
            rb.velocity = transform.forward * speed;
        }
    }

    private void ChargeTowardsPlanet()
    {
        speed = chargeSpeed;
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
            Destroy(gameObject);
            GameObject explosionEffectGO = Instantiate(explosionParticle, transform.position, transform.rotation);
            Destroy(explosionEffectGO, 2f);
            planetHealthManager.TakeDamage(25);
        }

        if (collision.gameObject.CompareTag("ShipProjectile"))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        health -= 1f;
        if(health <= 0)
        {
            GameManager.Instance.Score += killReward;
            Destroy(gameObject);
            GameObject explosionEffectGO = Instantiate(explosionParticle, transform.position, transform.rotation);
            Destroy(explosionEffectGO, 2f);
        }
    }
}
