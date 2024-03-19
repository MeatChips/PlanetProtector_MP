using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject hitmarker;
    private float destroyTimer = 30f;

    private void Update()
    {
        destroyTimer -= Time.deltaTime;
        if (destroyTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject Hitmarker = Instantiate(hitmarker, transform.position, transform.rotation);
            Destroy(Hitmarker, .1f);
            Destroy(this.gameObject);
        }
    }
}
