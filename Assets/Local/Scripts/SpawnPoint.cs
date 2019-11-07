using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject animal;
    public int spawnTime = 5;
    public int maxAlive = 5;
    public float minScale = 1.0f;
    public float maxScale = 1.0f;
    public float spawnRadius = 20.0f;

    private int livingAnimals = 0;

    private void Start()
    {
        InvokeRepeating("Spawn", spawnTime * TickManager.global.tickDuration, spawnTime * TickManager.global.tickDuration);
    }

    void Spawn()
    {
        if (livingAnimals < maxAlive)
        {
            float scaleFactor = Random.Range(minScale, maxScale);
            animal.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

            float spawnPosX = Random.Range(-spawnRadius, spawnRadius);
            float spawnPosZ = Random.Range(-spawnRadius, spawnRadius);
 
            Instantiate(animal, transform.position + new Vector3(spawnPosX, transform.position.y, spawnPosZ), transform.rotation);
            livingAnimals++;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    public void OnKillAnimal()
    {
        livingAnimals--;
    }
}
