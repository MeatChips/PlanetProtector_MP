using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingEnemy : MonoBehaviour
{
    [SerializeField] private Transform target; // The target it has to fly too and shoot (the planet)
    [SerializeField] private Transform cannonRotationPoint; // The rotation point for the guns, so they can rotate
    //[SerializeField] private Transform[] firePoints; // The points were they projectiles get fired from
    [SerializeField] private float thrustSpeed = 5f; // The ship its speed
    [SerializeField] private float sphereColRadius = 100f;
    private Rigidbody rb;
    private SphereCollider sphereCol;
    private PlanetHealthManager planetHealthManager;

    [SerializeField] private Rigidbody rbProjectile; // Get the rigidbody instead of the entire gameobject, so you wont have to reference to the rigidbody inside the script.
    [SerializeField] private float launchForce = 5000f; // The force the projectile gets shot with
    [SerializeField] private Transform[] firePoints; // The points were they projectiles get fired from

    [SerializeField] private float timeBetweenShooting = 5f;

    private bool canMove, readyToShoot, detectedTarget;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCol = GetComponent<SphereCollider>();

        target = GameObject.FindGameObjectWithTag("Target").transform;
        planetHealthManager = GameObject.FindGameObjectWithTag("Target").GetComponent<PlanetHealthManager>();

        sphereCol.radius = sphereColRadius;

        canMove = true;
        readyToShoot = false;
        detectedTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) MoveTowardsTarget();
        if (detectedTarget) RotateGunsTowardTarget();
        if (readyToShoot) LaunchProjectile();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Target"))
        {
            canMove = false;
            detectedTarget = true;
            if(detectedTarget) readyToShoot = true;
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

    // Rotate guns
    private void RotateGunsTowardTarget()
    {
        cannonRotationPoint.LookAt(target);
    }

    
    // Firing projectiles
    private void LaunchProjectile()
    {
        foreach (var firePoint in firePoints)
        {
            var projectileInstance = Instantiate(rbProjectile, firePoint.position, firePoint.rotation); // Spawn the projectile at the firepoints
            projectileInstance.AddForce(firePoint.forward * launchForce); // Add the launchforce so it goes forward
            planetHealthManager.TakeDamage(7);
        }

        readyToShoot = false;
        Invoke("ResetLaunch", timeBetweenShooting);
    }

    private void ResetLaunch()
    {
        readyToShoot = true;
    }
}
