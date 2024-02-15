using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private float rateToSpawn = 0.5f;
    private float nextSpawn = 0f;

    private bool isSpawning;

    // Start is called before the first frame update
    void Start()
    {
        isSpawning = true;
    }

    // Update is called once per frame
    void Update()
    {
        // if isSpawning is true, it allowed to start spawning
        if (isSpawning)
        {
            StartSpawning();
        }
    }

    private void StartSpawning()
    {
        int randomIndex = Random.Range(0, Enemies.Length); // Random enemy 

        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + rateToSpawn; // Next spawn is the time it is now + rateToSpawn

            // Random position within this transform
            Vector3 rndPosWithin;
            rndPosWithin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            rndPosWithin = transform.TransformPoint(rndPosWithin * .5f); // Set a random position within the object's transform
            Instantiate(Enemies[randomIndex], rndPosWithin, transform.rotation); // Spawn the enemy
        }
    }
}
