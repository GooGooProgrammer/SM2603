using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrepareStageUI : MonoBehaviour
{
    public static PrepareStageUI Instance;

    //crystal
    [SerializeField]
    private GameObject crystalPrefab;
    [SerializeField]
    private Transform crystal_List;

    [SerializeField]
    private int crystalCount;

    public void AddCrystal()
    {
        crystalCount++;
        if(crystalCount<=0) return;
        GameObject crystal = Instantiate(crystalPrefab,crystal_List);
        crystal.GetComponent<RectTransform>().anchoredPosition += Vector2.left * 50 * crystal_List.childCount;
    }

    public bool ReduceOneCrystal()
    {
        crystalCount--;
        if (crystalCount >= 0)
        {
            Destroy(crystal_List.GetChild(crystal_List.childCount-1).gameObject);
            return true;
        }
        return false;
        //return false when crystal not enough
    }

    //end of crystal

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        AddCrystal();
    }
}
