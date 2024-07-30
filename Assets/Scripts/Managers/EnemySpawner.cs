using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnData
{
    public ElementType elementType;
    public CombatType combatType;
}

public class WaveData
{
    public List<EnemySpawnData> enemyList;

    public WaveData()
    {
        enemyList = new List<EnemySpawnData>();
    }
}

public class EnemySpawner : MonoBehaviour
{
    public List<Enemy> spawnedEnemies;
    public List<Transform> spawnLocations;

    public GameObject EnemyPrefab;

    private const int baseNumElementInWave = 1;
    private const int baseNumTypeInWave = 1;
    private const int baseNumEnemiesInWaveMin = 1;
    private const int baseNumEnemiesInWaveMax = 5;
    
    private const int baseNumWaves = 1;

    private List<WaveData> currentWaves; 

    // Start is called before the first frame update
    private void Awake()
    {
        EventManager.OnStartCombat += StartCombat;
    }

    private void OnDestroy()
    {
        EventManager.OnStartCombat -= StartCombat;
    }

    private void StartCombat(int _roomNumber)
    {
        GenerateWaves(_roomNumber);
        StartCoroutine(RunCombat());
    }
    public void GenerateWaves(int _roomNumber)
    {
        int elementTypesInWave = baseNumElementInWave * _roomNumber;
        int combatTypesInWave = baseNumTypeInWave * _roomNumber;
        int maxEnemiesInWave = baseNumEnemiesInWaveMax + _roomNumber;
        int minEnemiesInWave = baseNumEnemiesInWaveMin + _roomNumber;
        int numWaves = baseNumWaves * _roomNumber;

        currentWaves = new List<WaveData>();
        List<CombatType> combatTypes = new List<CombatType>();
        List<ElementType> elementTypes = new List<ElementType>();

        //Get all the types
        combatTypes = Enum.GetValues(typeof(CombatType)).Cast<CombatType>().ToList();
        elementTypes = Enum.GetValues(typeof(ElementType)).Cast<ElementType>().ToList();

        combatTypes.Remove(CombatType.Empty);
        elementTypes.Remove(ElementType.Empty);


        //Randomise the Lists
        //combatTypes = combatTypes.OrderBy(_x => Guid.NewGuid()).ToList();
        elementTypes = elementTypes.OrderBy(_y => Guid.NewGuid()).ToList();

        elementTypes = elementTypes.Take(elementTypesInWave).ToList();
        combatTypes = combatTypes.Take(combatTypesInWave).ToList();

        for (int i = 0; i < numWaves; i++)
        {
            WaveData waveData = new WaveData();
            int enemiesInWave = UnityEngine.Random.Range(minEnemiesInWave, maxEnemiesInWave);

            //generate the enemies

            for (int x = 0; x < enemiesInWave; x++)
            {
                //generate the enemy
                EnemySpawnData enemyData = new()
                {
                    elementType = elementTypes[UnityEngine.Random.Range(0, elementTypes.Count)],
                    combatType = combatTypes[UnityEngine.Random.Range(0, combatTypes.Count)]
                };

                waveData.enemyList.Add(enemyData);
            }

            currentWaves.Add(waveData);
        }
    }

    public void RunWaveTimer()
    {



    //    if (spawnTimer <= 0) 
    //    { 
            
            
    //        if (enemiesToSpawn.Count > 0)
    //        {
    //            Vector3 randomPos = spawnLocations[UnityEngine.Random.Range(0, spawnLocations.Count)].position;
    //            Enemy enemy = Instantiate(enemiesToSpawn[0], randomPos, Quaternion.identity);

    //            enemy.SetElementType( ElementType.Earth);
    //            enemy.SetCombatType(CombatType.Mage);
    //            enemy.SetStats(1,0,0);
    //            enemiesToSpawn.RemoveAt(0);
    //            spawnTimer = spawnInterval;

    //            AIManager.Instance.activeEnemies.Add(enemy);


    //        }
        
    //    }
    //    else
    //    {

    //        spawnTimer -= Time.deltaTime;


    //    }



    }

    private IEnumerator RunCombat()
    {
        for (int i = 0; i < currentWaves.Count; i++)
        {
            SpawnWave(currentWaves[i]);
            while(spawnedEnemies.Count > 0)
            {
                yield return new WaitForSeconds(0.1f);
                ValidateSpawnedEnemies();
            }
            Debug.Log("Wave is Over");
        }

        EventManager.CombatCompleted();
        Debug.Log("Combat is Over");
    }

    private void SpawnWave(WaveData waveData)
    {
        foreach (var item in waveData.enemyList)
        {
            SpawnEnemy(item);
        }
    }

    private void SpawnEnemy(EnemySpawnData enemyData)
    {
        Vector3 randomPos = spawnLocations[UnityEngine.Random.Range(0, spawnLocations.Count)].position;
        Enemy enemy = Instantiate(EnemyPrefab, randomPos, Quaternion.identity).GetComponent<Enemy>();

        enemy.SetElementType(enemyData.elementType);
        enemy.SetCombatType(enemyData.combatType);
        enemy.SetStats(1, 0, 0);
        
        

        AIManager.Instance.activeEnemies.Add(enemy);
    }

    private void ValidateSpawnedEnemies()
    {
        spawnedEnemies = spawnedEnemies.Where(_x => !_x.isDead && _x != null).ToList();
    

    }
}
public static class ListExtensions
{
    public static T GetRandomElement<T>(this List<T> _list)
    {
        int index = UnityEngine.Random.Range(0, _list.Count);

        return _list[index];
    }
}



