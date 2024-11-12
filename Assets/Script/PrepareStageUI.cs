using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareStageUI : MonoBehaviour
{
    public static PrepareStageUI Instance;
    public GameObject StartBtn;

    void Awake()
    {
        Instance = this;
    }
}
