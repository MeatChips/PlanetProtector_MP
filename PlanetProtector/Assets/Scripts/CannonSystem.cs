using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSystem : MonoBehaviour
{
    [SerializeField] private Transform[] firePoints; // The points were they projectiles get fired from
    [SerializeField] private Rigidbody rb; // Get the rigidbody instead of the entire gameobject, so you wont have to reference to the rigidbody inside the script.
    [SerializeField] private float launchForce = 5000f; // The force the projectile gets shot with

    // Update is called once per frame
    void Update()
    {
        // Press left mouse button to fire
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            LaunchProjectile();
        }
    }

    private void LaunchProjectile()
    {
        foreach (var firePoint in firePoints)
        {
            var projectileInstance = Instantiate(rb, firePoint.position, firePoint.rotation); // Spawn the projectile at the firepoints
            projectileInstance.AddForce(firePoint.forward * launchForce); // Add the launchforce so it goes forward
        }
    }
}
