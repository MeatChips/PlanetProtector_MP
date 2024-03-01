using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : MonoBehaviour
{
    [SerializeField] private Transform target; // The target it has to fly too and shoot (the planet)
    [SerializeField] private Transform cannonRotationPoint; // The rotation point for the guns, so they can rotate
    [SerializeField] private float thrustSpeed = 5f; // The ship its speed
    [SerializeField] private float sphereColRadius = 100f; // Radius of detection sphere
    private Rigidbody rb;
    private SphereCollider sphereCol;

    [SerializeField] private Transform laserOrigin; // Origin from where to laser is getting shot
    [SerializeField] private LineRenderer laserLine; // The laser
    [SerializeField] private float laserTime = 10f; // Time the laser is on
    [SerializeField] private float timeBetweenShooting = 30f;

    private bool canMove, readyToShoot, detectedTarget;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCol = GetComponent<SphereCollider>();

        target = GameObject.FindGameObjectWithTag("Target").transform;

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
        if (readyToShoot) ShootLaser();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            canMove = false;
            detectedTarget = true;
            if (detectedTarget) readyToShoot = true;
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
    private void ShootLaser()
    {
        Vector3 rayOrigin = laserOrigin.transform.position;
        RaycastHit hit;
        Vector3 direction = target.transform.position;

        laserLine.SetPosition(0, laserOrigin.position);
        if(Physics.Raycast(rayOrigin, direction, out hit))
        {
            //Debug.Log("HIT");
            laserLine.SetPosition(1, hit.point);
            laserTime -= Time.deltaTime;
        }

        if(laserTime <= 0)
        {
            laserLine.enabled = false;
            readyToShoot = false;
            Invoke("ResetLaunch", timeBetweenShooting);
        }
    }

    private void ResetLaunch()
    {
        laserTime = 5f;
        laserLine.enabled = true;
        readyToShoot = true;
    }
}
