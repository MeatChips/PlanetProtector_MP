using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject hitSound;
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
            GameObject hitSoundGO = Instantiate(hitSound, transform.position, transform.rotation);
            Destroy(hitSoundGO, .5f);
            Destroy(this.gameObject);
        }
    }
}
