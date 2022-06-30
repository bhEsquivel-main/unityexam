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

        Debug.Log("SPAWN: "+ this.gameObject.name);
        InvokeRepeating("Spawn", 0, spawnTime);
    }

    public void StopSpawning()
    {
        CancelInvoke("Spawn");
    }

    public Transform[] SPAWN_POINTS
    {
        get {   
            if(spawnPoints ==null || spawnPoints.Length  == 0) {
                spawnPoints = GetComponentsInChildren<Transform>();
            }
            return spawnPoints;
        }
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
            return (this.SPAWN_POINTS[Random.Range(0, this.SPAWN_POINTS.Length)]);
        }
    }

    void Spawn() 
    {
        if(this.gameObject.name.Contains("Enemy")) {
            Debug.Log("SPAWN: "+ this.SPAWN_POINTS.Length + " : " + this.spawnData.Length);
        }
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
