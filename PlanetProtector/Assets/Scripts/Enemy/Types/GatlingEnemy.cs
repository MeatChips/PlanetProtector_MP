using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingEnemy : MonoBehaviour
{
    [SerializeField] private Transform target; // The target it has to fly too and shoot (the human base)
    [SerializeField] private GameObject[] targets; // Array of targets
    [SerializeField] private float thrustSpeed = 5f; // The ship its speed
    [SerializeField] private float sphereColRadius = 100f;
    [SerializeField] private GameObject explosionParticle;

    private Rigidbody rb;
    private SphereCollider sphereCol;
    private PlanetHealthManager planetHealthManager;
    private AudioSource audioSource;

    [SerializeField] private Rigidbody rbProjectile; // Get the rigidbody instead of the entire gameobject, so you wont have to reference to the rigidbody inside the script.
    [SerializeField] private float launchForce = 5000f; // The force the projectile gets shot with
    [SerializeField] private Transform[] firePoints; // The points were they projectiles get fired from

    [SerializeField] private float timeBetweenShooting = 5f;

    private float health = 14f;
    private int killReward = 6;

    private bool canMove, readyToShoot;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCol = GetComponent<SphereCollider>();
        audioSource = GetComponent<AudioSource>();

        if (!GameManager.Instance.gameEnded)
        {
            targets = GameObject.FindGameObjectsWithTag("Target");
            target = targets[Random.Range(0, targets.Length)].transform;
/*            target = GameObject.FindGameObjectWithTag("Target").transform;*/
            planetHealthManager = GameObject.FindGameObjectWithTag("Target").transform.parent.GetComponent<PlanetHealthManager>();
        }

        sphereCol.radius = sphereColRadius;

        canMove = true;
        readyToShoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !GameManager.Instance.gamePaused && !GameManager.Instance.gameEnded) MoveTowardsTarget();
        if (readyToShoot && !GameManager.Instance.gamePaused && !GameManager.Instance.gameEnded) LaunchProjectile();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Target"))
        {
            canMove = false;
            readyToShoot = true;
        }
    }

    // Move ship
    private void MoveTowardsTarget()
    {
        transform.LookAt(target); // Look at the target (the planet)
        if (target != null)
        {
            rb.velocity = transform.forward * thrustSpeed;
        }
    }

    
    // Firing projectiles
    private void LaunchProjectile()
    {
        foreach (var firePoint in firePoints)
        {
            var projectileInstance = Instantiate(rbProjectile, firePoint.position, firePoint.rotation); // Spawn the projectile at the firepoints
            projectileInstance.AddForce(firePoint.forward * launchForce); // Add the launchforce so it goes forward
            planetHealthManager.TakeDamage(7);
            audioSource.Play();
        }

        readyToShoot = false;
        Invoke("ResetLaunch", timeBetweenShooting);
    }

    private void ResetLaunch()
    {
        readyToShoot = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
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
            GameObject explosionEffectGO = Instantiate(explosionParticle, transform.position, transform.rotation);
            Destroy(explosionEffectGO, 2f);
            GameManager.Instance.Score += killReward;
            Destroy(gameObject);
        }
    }
}
