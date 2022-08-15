using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Configuration")]
public class WareConfig : ScriptableObject
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] GameObject PathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 10;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab() {return EnemyPrefab; }
    public List<Transform> GetWayPoints() 
    {
        var waveWayPoints = new List<Transform>();
        foreach (Transform child in PathPrefab.transform)
        {
            waveWayPoints.Add(child);
        }
        return waveWayPoints; ; 
    }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }
    public int GetNumberOfEnemy() { return numberOfEnemies; }
    public float GetMoveSpeed() { return moveSpeed; }
}
