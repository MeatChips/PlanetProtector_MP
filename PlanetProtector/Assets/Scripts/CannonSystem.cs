using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSystem : MonoBehaviour
{
    [SerializeField] private Transform[] firePoints;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float launchForce = 700f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            LaunchProjectile();
        }
    }

    private void LaunchProjectile()
    {
        foreach (var firePoint in firePoints)
        {
            var projectileInstance = Instantiate(rb, firePoint.position, firePoint.rotation);
            projectileInstance.AddForce(firePoint.forward * launchForce);
        }
    }
}
