using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : MonoBehaviour
{
    [SerializeField] private Transform target; // The target it has to fly too and shoot (the human base)
    [SerializeField] private GameObject[] targets; // Array of targets
    [SerializeField] private GameObject explosionParticle;
    [SerializeField] private GameObject explosionSound;
    private float thrustSpeed = 15f; // The ship its normal speed
    private float chargeSpeed = 60f; // The ship its charge speed
    private float sphereColRadius = 100f; // Radius of detection sphere

    private float speed;
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
            GameObject explosionEffectGO = Instantiate(explosionParticle, transform.position, transform.rotation);
            Destroy(explosionEffectGO, 2f);
            GameObject explosionSoundGO = Instantiate(explosionSound, transform.position, transform.rotation);
            Destroy(explosionSoundGO, 2f);
            planetHealthManager.TakeDamage(25);
            Destroy(gameObject);
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
            GameObject explosionEffectGO = Instantiate(explosionParticle, transform.position, transform.rotation);
            Destroy(explosionEffectGO, 2f);
            GameManager.Instance.Score += killReward;
            Destroy(gameObject);
        }
    }
}
