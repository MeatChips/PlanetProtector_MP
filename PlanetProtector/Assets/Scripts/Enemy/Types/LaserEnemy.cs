using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : MonoBehaviour
{
    [SerializeField] private Transform target; // The target it has to fly too and shoot (the human base)
    [SerializeField] private GameObject[] targets; // Array of targets
    [SerializeField] private GameObject explosionParticle;
    private float thrustSpeed = 5f; // The ship its speed
    private float sphereColRadius = 100f; // Radius of detection sphere
    private Rigidbody rb;
    private SphereCollider sphereCol;

    private PlanetHealthManager planetHealthManager;
    private ShipController shipController;
    private List<int> laserTickTimers = new List<int>();

    [SerializeField] private Transform laserOrigin; // Origin from where to laser is getting shot
    [SerializeField] private LineRenderer laserLine; // The laser
    private float laserTime = 10f; // Time the laser is on
    private float timeBetweenShooting = 30f;

    private float health = 30f;
    private int killReward = 14;

    private bool canMove, readyToShoot;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCol = GetComponent<SphereCollider>();
        shipController = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipController>();

        if (!GameManager.Instance.gameEnded)
        {
            targets = GameObject.FindGameObjectsWithTag("Target");
            target = targets[Random.Range(0, targets.Length)].transform;
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
        if (readyToShoot && !GameManager.Instance.gamePaused && !GameManager.Instance.gameEnded) ShootLaser();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
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
    private void ShootLaser()
    {
        Vector3 rayOrigin = laserOrigin.transform.position;
        RaycastHit hit;
        Vector3 direction = target.transform.position;

        laserLine.SetPosition(0, laserOrigin.position);
        if(Physics.Raycast(rayOrigin, direction, out hit))
        {
            laserLine.SetPosition(1, hit.point);
            laserTime -= Time.deltaTime;
            StartLaserDamage(4);
            if (hit.collider.CompareTag("Player"))
            {
                shipController.ShipDeath();
            }
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
            GameObject explosionEffectGO = Instantiate(explosionParticle, transform.position, transform.rotation);
            Destroy(explosionEffectGO, 2f);
            GameManager.Instance.Score += killReward;
            Destroy(gameObject);
        }
    }
}
