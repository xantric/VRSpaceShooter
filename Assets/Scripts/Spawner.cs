using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] asteroidPrefab;
    public float spawnRadius;
    public float spawnInterval;
    public float minSize;
    public float maxSize;
    public float minSpeed;
    public float maxSpeed;
    public Transform player;

    void Start()
    {
        StartCoroutine(SpawnAsteroids());
    }

    IEnumerator SpawnAsteroids()
    {
        yield return new WaitForSeconds(6.0f);
        while (true)
        {
            GameObject asteroid = SpawnAsteroid();
            yield return new WaitForSeconds(spawnInterval);
            // Optional: Decrease spawn interval over time for difficulty scaling
            spawnInterval = Mathf.Max(0.5f, spawnInterval - 0.01f);
            DestroyAsteroid(asteroid);
        }
    }
    Vector3 SpawnPosition()
    {
        float theta = Random.Range(0, Mathf.PI / 3);
        float phi = Random.Range(0, Mathf.PI);
        
        float x = spawnRadius * Mathf.Cos(phi) * Mathf.Sin(theta);
        float y = spawnRadius * Mathf.Sin(phi) * Mathf.Sin(theta);
        float z = spawnRadius * Mathf.Cos(theta);
        return new Vector3(x, y, z);
    }
    GameObject SpawnAsteroid()
    {
        Vector3 spawnPosition = SpawnPosition();
        GameObject asteroid = Instantiate(asteroidPrefab[Random.Range(0, asteroidPrefab.Length)], spawnPosition, Quaternion.identity);
        asteroid.AddComponent<AudioSource>();
        float size = Random.Range(minSize, maxSize);
        asteroid.transform.localScale = Vector3.one * size;

        Rigidbody rb = asteroid.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (player.position - spawnPosition).normalized;
            float speed = Random.Range(minSpeed, maxSpeed);
            rb.velocity = direction * speed;
        }
        return asteroid;
    }
    void DestroyAsteroid(GameObject asteroid)
    {
        
        if (asteroid != null && asteroid.transform.position.z < -(10))
        {
            
            Destroy(asteroid);
        }
    }
}
