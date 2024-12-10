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

    List<int> currentTimeSpawn;
    int currentTime = 1;
    public int currentWave  {private set;get;} = 0;

    public void StartWave()
    {
        InvokeRepeating("CheckSpawnEnemy", 0f, 1f);
        currentTime = 1;
        currentTimeSpawn = new List<int>();
        foreach(EnemyListObject e in waveList[currentWave].enemyList)
        {
            currentTimeSpawn.Add(e.timeSpawn);
        }
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

    public void ClearAllEnemy()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public (GameObject, int) GetEnemySet(int i)
    {
        return (
            waveList[currentWave].enemyList[i].enemy,
            waveList[currentWave].enemyList[i].numEnemy
                * waveList[currentWave].enemyList[i].timeSpawn
        );
    }

    public int GetEnemyListLength()
    {
        return waveList[currentWave].enemyList.Count;
    }

    public void CurrentWavePlus1()
    {

        currentWave++;
        currentTimeSpawn = new List<int>();
        foreach(EnemyListObject e in waveList[currentWave].enemyList)
        {
            currentTimeSpawn.Add(e.timeSpawn);
        }
    }
    public void ResetWave()
    {
        for (int i = 0;i <  waveList[currentWave].enemyList.Count; i++)
        {
            waveList[currentWave].enemyList[i].timeSpawn = currentTimeSpawn[i];
        }
        EnemySetControl.Instance.CleanEnemySetList();
        EnemySetControl.Instance.SpawnEnemySet();
    }
    IEnumerator SpawnEnemy(EnemyListObject e)
    {
        if(GameManager.Instance.state != GameState.Fight) yield break;
        for (int i = 0; i < e.numEnemy; i++)
        {
            if(GameManager.Instance.state != GameState.Fight) yield break;
            float randomY = UnityEngine.Random.Range(0, Camera.main.orthographicSize / 2);
            Instantiate(
                e.enemy,
                transform.position + new Vector3(0,  randomY, 0),
                Quaternion.identity,
                transform
            );
            yield return new WaitForSeconds(1.5f);
        }
        e.timeSpawn--;
    }
}
