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

    private PlanetHealthManager planetHealthManager;
    public List<int> laserTickTimers = new List<int>();

    [SerializeField] private Transform laserOrigin; // Origin from where to laser is getting shot
    [SerializeField] private LineRenderer laserLine; // The laser
    [SerializeField] private float laserTime = 10f; // Time the laser is on
    [SerializeField] private float timeBetweenShooting = 30f;

    private float health = 30f;
    private int killReward = 14;

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
            StartLaserDamage(4);
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
        laserTime = 10f;
        laserLine.enabled = true;
        readyToShoot = true;
    }

    public void StartLaserDamage(int ticks)
    {
        if(laserTickTimers.Count <= 0)
        {
            laserTickTimers.Add(ticks);
            StartCoroutine(DamageOverTime());
        }
        else
        {
            laserTickTimers.Add(ticks);
        }
    }

    public IEnumerator DamageOverTime()
    {
        while(laserTickTimers.Count > 0)
        {
            for (int i = 0; i < laserTickTimers.Count; i++)
            {
                laserTickTimers[i]--;
            }
            planetHealthManager.TakeDamage(2);
            laserTickTimers.RemoveAll(i => i == 0);
            yield return new WaitForSeconds(1f);
        }
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
            GameManager.Instance.playerScore += killReward;
            Destroy(this.gameObject);
        }
    }
}
