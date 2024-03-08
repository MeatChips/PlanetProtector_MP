using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private float minRateToSpawn = 2f;
    [SerializeField] private float maxRateToSpawn = 6f;
    [SerializeField] private GameObject outerCircle;
    private float outerCircleRotationSpeed = 20f;
    private float nextSpawn = 0f;

    public bool isSpawning, newEnemy;
    public int maxEnemies;

    [SerializeField] private List<GameObject> SpawnedEnemies = new List<GameObject>();

    void Start()
    {
        newEnemy = true;
    }

    // Update is called once per frame
    void Update()
    {
        outerCircle.transform.Rotate(Vector3.up * outerCircleRotationSpeed * Time.deltaTime);

        // Check if the GameManager says that the game is started, then start spawning.
        if (GameManager.Instance.gameStarted)
            isSpawning = true;

        if (GameManager.Instance.gamePaused)
            isSpawning = false;
        else if (!GameManager.Instance.gamePaused)
            isSpawning = true;

        // if isSpawning is true, it allowed to start spawning
        if (isSpawning)
            if (newEnemy)
                StartSpawning();

        if (SpawnedEnemies.Count >= maxEnemies) newEnemy = false;
        else newEnemy = true;
    }

    private void StartSpawning()
    {
        int randomIndex = Random.Range(0, Enemies.Length); // Random enemy 

        // Random position within this transform
        Vector3 rndPosWithin;
        rndPosWithin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rndPosWithin = transform.TransformPoint(rndPosWithin * .5f); // Set a random position within the object's transform

        SpawnedEnemies.RemoveAll(GameObject => GameObject == null);

        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + Random.Range(minRateToSpawn, maxRateToSpawn); // Next spawn is the time it is now + rateToSpawn

            GameObject enemy = Instantiate(Enemies[randomIndex], rndPosWithin, transform.rotation); // Spawn the enemy
            SpawnedEnemies.Add(enemy);
        }
    }
}
