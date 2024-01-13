using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
  // public Transform pos;

  //  public GameObject[] objectsToInstantiate;

    public Alien alienPrefab;

    public float trajectoryVariance = 15.0f;

    public float spawnRate = 2.0f;

    public float spawnDistance = 15.0f;

    public int spawnAmount = 1;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
       
       // int n = Random.Range(0, objectsToInstantiate.Length);
       
      // GameObject g = Instantiate(objectsToInstantiate[n], pos.position, objectsToInstantiate[n].transform.rotation);
    }

    private void Spawn()
    {
        

        for  (int i = 0; i < this.spawnAmount; i++)  
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);


            
            Alien alien = Instantiate(this.alienPrefab,spawnPoint, rotation);
            alien.size = Random.Range(alien.minSize, alien.maxSize);
            alien.SetTrajectory(rotation * -spawnDirection);
        
        }
    }
}
