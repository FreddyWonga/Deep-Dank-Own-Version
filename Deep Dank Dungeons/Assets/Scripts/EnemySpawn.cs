using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawns;
    public int spawnAmount = 2;

    private int spawnIndex = 0;

    //Spawns chosen number of enemies in the spawn locations in a room
    public void EnemySpawner()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(enemyPrefab, spawns[spawnIndex].transform.position, spawns[spawnIndex].transform.rotation);
            spawnIndex++;
            if(spawnIndex >= spawnAmount) 
            {
                spawnIndex = 0;
            }
        }
    }
}
