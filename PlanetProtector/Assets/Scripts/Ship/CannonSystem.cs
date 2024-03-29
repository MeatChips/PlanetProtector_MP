using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSystem : MonoBehaviour
{
    [SerializeField] private Rigidbody rb; // Get the rigidbody instead of the entire gameobject, so you wont have to reference to the rigidbody inside the script.
    [SerializeField] private Transform[] firePoints; // The points were they projectiles get fired from
    [SerializeField] private OverheatBar overheatBar;
    private float launchForce = 10000f; // The force the projectile gets shot with
    private AudioSource audioSource;

    private int maxHeat = 100;
    private int currentHeat;
    private float rateOfCooldown = 0.5f;
    private float nextCooldown = 0f;

    private float timeBetweenShooting = 0.2f;

    public bool allowButtonHold;
    private bool shooting, readyToShoot, isOverheated;

    void Start()
    {
        currentHeat = maxHeat;
        overheatBar.SetMaxNumber(maxHeat);
        readyToShoot = true;
        isOverheated = false;

        audioSource = GetComponent<AudioSource>();
        Physics.IgnoreCollision(rb.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (readyToShoot && shooting && !isOverheated && !GameManager.Instance.gamePaused && !GameManager.Instance.gameEnded && GameManager.Instance.gameStarted)
        {
            LaunchProjectile();
            Heating(2);
        }

        if (Time.time > nextCooldown && isOverheated)
        {
            nextCooldown = Time.time + rateOfCooldown;
            Cooling(6);
        }

        if (Time.time > nextCooldown && !shooting)
        {
            nextCooldown = Time.time + rateOfCooldown;
            Cooling(6);
        }
        
        if(currentHeat >= 100)
        {
            currentHeat = 100;
        }
    }

    private void LaunchProjectile()
    {
        foreach (var firePoint in firePoints)
        {
            var projectileInstance = Instantiate(rb, firePoint.position, firePoint.rotation); // Spawn the projectile at the firepoints
            projectileInstance.AddForce(firePoint.forward * launchForce); // Add the launchforce so it goes forward
            audioSource.Play();
        }

        if (allowButtonHold)
        {
            readyToShoot = false;
            Invoke("ResetLaunch", timeBetweenShooting);
        }
    }

    private void ResetLaunch()
    {
        readyToShoot = true;
    }

    private void Heating(int heat)
    {
        currentHeat -= heat;
        overheatBar.SetNumber(currentHeat);

        if (currentHeat <= 0)
            isOverheated = true;
    }

    private void Cooling(int cool)
    {
        currentHeat += cool;
        overheatBar.SetNumber(currentHeat);

        if (currentHeat >= 40)
        {
            isOverheated = false;
        }
    }
}
