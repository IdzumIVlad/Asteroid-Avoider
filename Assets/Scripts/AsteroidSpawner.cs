using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs;
    [SerializeField] private float secondsBetweenAsteroids;
    [SerializeField] private Vector2 forceRange = new Vector2 (4, 6);

    private Camera mainCamera;
    private float timer;

    private void Start()
    {
        mainCamera = Camera.main;
        secondsBetweenAsteroids = 1.5f;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnNewAsteroid();
            timer += secondsBetweenAsteroids;
            if(secondsBetweenAsteroids >= 0.6f)
                secondsBetweenAsteroids -= 0.05f;
        }
    }

    private void SpawnNewAsteroid()
    {
        int side = UnityEngine.Random.Range(0, 4);
        Vector2 spawnPoint = Vector2.zero;
        Vector2 direction = Vector2.zero;

        switch (side)
        {
            case 0:
                spawnPoint.x = 0;
                spawnPoint.y = UnityEngine.Random.value;
                direction = new Vector2(1f, UnityEngine.Random.Range(-1f, 1f));
                break;
            case 1:
                spawnPoint.x = 1;
                spawnPoint.y = UnityEngine.Random.value;
                direction = new Vector2(-1f, UnityEngine.Random.Range(-1f, 1f));
                break;
            case 2:
                spawnPoint.x = UnityEngine.Random.value;
                spawnPoint.y = 0;
                direction = new Vector2(UnityEngine.Random.Range(-1f, 1f), 1f);
                break;
            case 3:
                spawnPoint.x = UnityEngine.Random.value;
                spawnPoint.y = 1;
                direction = new Vector2(UnityEngine.Random.Range(-1f, 1f), -1f);
                break;
        }

        Vector3 worldSpawnPoint = mainCamera.ViewportToWorldPoint(spawnPoint);
        worldSpawnPoint.z = 0;
        GameObject SelectedAsteroid = asteroidPrefabs[UnityEngine.Random.Range(0, asteroidPrefabs.Length)];
        GameObject asteroidInstance = Instantiate(SelectedAsteroid,
            worldSpawnPoint,
            Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));

        Rigidbody rb = asteroidInstance.GetComponent<Rigidbody>();
        rb.velocity = direction.normalized * UnityEngine.Random.Range(forceRange.x, forceRange.y);

    }
}
