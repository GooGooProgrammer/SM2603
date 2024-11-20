using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrepareStageUI : MonoBehaviour
{
    public static PrepareStageUI Instance;

    //crystal
    [SerializeField]
    private TextMeshProUGUI crystal;
    private int crystalCount = 1;
    public void AddCrystal(int num)
    {
        crystalCount = crystalCount + num;
        crystal.text = crystalCount.ToString();
    }
    public bool ReduceOneCrystal()
    {
        crystalCount--;
        if (crystalCount>=0)
        {
            crystal.text = crystalCount.ToString();
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
}
