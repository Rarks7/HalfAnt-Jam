using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public int currentWave;
    public int maxWaves;
    public float waveInterval;
    public float waveTimer;


    public List<Enemy> enemiesToSpawn;


    public List<Transform> spawnLocations;
    public float spawnInterval;
    public float spawnTimer;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RunWaveTimer();
    }


    public void GenerateWave()
    {



    }



    public void RunWaveTimer()
    {




        if (spawnTimer <= 0) 
        { 
            
            
            if (enemiesToSpawn.Count > 0)
            {
                Vector3 randomPos = spawnLocations[Random.Range(0, spawnLocations.Count)].position;
                Enemy enemy = Instantiate(enemiesToSpawn[0], randomPos, Quaternion.identity);
                enemiesToSpawn.RemoveAt(0);
                spawnTimer = spawnInterval;

                AIManager.Instance.activeEnemies.Add(enemy);


            }
        
        }
        else
        {

            spawnTimer -= Time.deltaTime;


        }



    }

}
