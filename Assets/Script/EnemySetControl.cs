using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySetControl : MonoBehaviour
{
    public static EnemySetControl Instance;

    void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    GameObject EnemySetPrefab;

    [SerializeField]
    Vector3 EnemySetPos;

    [SerializeField]
    float offsetY;


    List<GameObject> EnemeySetList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemySet();
    }

    // Update is called once per frame
    void Update() { }

    public void SpawnEnemySet()
    {
        for (int i = 0; i < EnemyControl.Instance.GetEnemyListLength(); i++)
        {
            EnemeySetList.Add(Instantiate(EnemySetPrefab, transform));

            (GameObject, int) enemySetData = EnemyControl.Instance.GetEnemySet(i);

            EnemeySetList[i].transform.localPosition = EnemySetPos + new Vector3(0, offsetY * i, 0);
            EnemeySetList[i].GetComponent<EnemySet>().image.sprite = enemySetData
                .Item1.GetComponent<SpriteRenderer>()
                .sprite;
            EnemeySetList[i].GetComponent<EnemySet>().image.color = enemySetData
                .Item1.GetComponent<SpriteRenderer>()
                .color;
            EnemeySetList[i].GetComponent<EnemySet>().count.text = enemySetData.Item2.ToString();
            EnemeySetList[i].GetComponent<EnemySet>().id = enemySetData
                .Item1.GetComponent<Enemy>()
                .GetId();
        }
    }

    public void EnemyNumberDecreaseOne(int id)
    {
        foreach (GameObject g in EnemeySetList)
        {
            if (g.GetComponent<EnemySet>().id == id)
            {
                int count = Int32.Parse(g.GetComponent<EnemySet>().count.text);
                count--;
                g.GetComponent<EnemySet>().count.text = count.ToString();
            }
        }
        CheckEnemyAllDead();
    }

    void CheckEnemyAllDead()
    {
        foreach (GameObject g in EnemeySetList)
        {
            if (g.GetComponent<EnemySet>().count.text != "0")
            {
                return;
            }
        }
        if(EnemyControl.Instance.currentWave == 2)
        {
            GameManager.Instance.WinTheGame();
            return;
        }
        PrepareStageUI.Instance.AddCrystal();
        EnemyControl.Instance.CancelInvoke();
        EnemyControl.Instance.CurrentWavePlus1();
        GameManager.Instance.SetGameState(GameState.Prepare);
        CleanEnemySetList();
        SpawnEnemySet();
    }
    public void CleanEnemySetList()
    {
        foreach (GameObject e in EnemeySetList)
        {
            Destroy(e);
        }
        EnemeySetList = new List<GameObject>();
    }
}
