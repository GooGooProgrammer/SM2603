using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
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

    void Start()
    {
        InvokeRepeating("CheckSpawnEnemy", 0f, 1f);
    }

    // Update is called once per frame
    void Update() { }

    void CheckSpawnEnemy()
    {
        foreach (EnemyListObject e in waveList[currentWave].enemyList)
        {
            if (currentTime % e.FrequencySpawn == 0)
            {
                StartCoroutine(SpawnEnemy(e));
            }
        }
        currentTime++;
    }

    void ClearEnemy(EnemyListObject e)
    {
        e.timeSpawn--;
        if (e.timeSpawn <= 0)
        {
            waveList[currentWave].enemyList.Remove(e);
        }
    }

    IEnumerator SpawnEnemy(EnemyListObject e)
    {
        
        for (int i = 0; i < e.numEnemy; i++)
        {
            float randomY = UnityEngine.Random.Range(0, Camera.main.orthographicSize / 2);
            Instantiate(e.enemy, transform.position + new Vector3(0, randomY , 0),Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
        }
        ClearEnemy(e);
    }
}
