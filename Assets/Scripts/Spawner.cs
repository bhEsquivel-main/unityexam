using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public delegate void SpawnerHandler(GameObject obj, GameSO data);
    public event SpawnerHandler OnSpawn;


    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private GameSO[] spawnData;

    public uint spawnTime = 3;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
    }

    public void StartSpawning() 
    {
        InvokeRepeating("Spawn", 5, spawnTime);
    }

    public void StopSpawning()
    {
        CancelInvoke("Spawn");
    }


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        
        
    }

    GameSO GetNextSpawner
    {
        get {
            return (this.spawnData[Random.Range(0, this.spawnData.Length)]);
        }
    }

    Transform GetRandomSpawnPoint
    {
        get {
            return (this.spawnPoints[Random.Range(0, this.spawnPoints.Length)]);
        }
    }

    void Spawn() 
    {
         Transform spawnPoint = GetRandomSpawnPoint;
         if(spawnPoint != null) 
         {
            GameSO spawnerData = GetNextSpawner;
            if (spawnerData) 
            {
                GameObject spawnedObj = Instantiate(spawnerData.prefab, spawnPoint.position, Quaternion.identity);
                OnSpawn?.Invoke(spawnedObj, spawnerData);
            }
         }
    }
    
}
