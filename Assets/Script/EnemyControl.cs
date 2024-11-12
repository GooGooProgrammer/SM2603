using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public static EnemyControl Instance;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    [Serializable]
    class EnemyListObject
    {
        public GameObject enemy;
        public int numEnemy;
        public int FrequencySpawn;
        public int timeSpawn;
    }

    [Serializable]
    class WaveListObject
    {
        public List<EnemyListObject> enemyList;

    }

    [SerializeField]
    List<WaveListObject> waveList;
    int currentTime = 1;
    int currentWave = 0;

    public void StartWave()
    {
        InvokeRepeating("CheckSpawnEnemy", 0f, 1f);
    }
    void CheckSpawnEnemy()
    {
        foreach (EnemyListObject e in waveList[currentWave].enemyList)
        {
            if (currentTime % e.FrequencySpawn == 0 && e.timeSpawn > 0)
            {
                StartCoroutine(SpawnEnemy(e));
            }
        }
        currentTime++;
    }

    void ClearEnemy(EnemyListObject e)
    {
        e.timeSpawn--;
        // if (e.timeSpawn <= 0)
        // {
        //     waveList[currentWave].enemyList.Remove(e);
        // }
    }
    public (GameObject, int) GetEnemySet(int i)
    {
        return (
            waveList[currentWave].enemyList[i].enemy,
            waveList[currentWave].enemyList[i].numEnemy * waveList[currentWave].enemyList[i].timeSpawn
        );
    }

    public int GetEnemyListLength()
    {
        return waveList[currentWave].enemyList.Count;
    }

    IEnumerator SpawnEnemy(EnemyListObject e)
    {
        for (int i = 0; i < e.numEnemy; i++)
        {
            float randomY = UnityEngine.Random.Range(0, Camera.main.orthographicSize / 2);
            Instantiate(
                e.enemy,
                transform.position + new Vector3(0, randomY, 0),
                Quaternion.identity
            );
            yield return new WaitForSeconds(1.5f);
            
        }
        ClearEnemy(e);
    }
}
