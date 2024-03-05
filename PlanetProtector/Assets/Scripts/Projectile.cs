using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float destroyTimer = 50f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        destroyTimer -= Time.deltaTime;
        if(destroyTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
