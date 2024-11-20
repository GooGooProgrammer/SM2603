using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeToggle : MonoBehaviour
{
    [SerializeField]
    Toggle toggle;
    void Start()
    {
        toggle.onValueChanged.AddListener(Upgrade);
    }
    void Upgrade(bool boolean)
    {
        switch (boolean)
        {
            case true:
                if(!PrepareStageUI.Instance.ReduceOneCrystal())
                {
                    toggle.isOn = false;
                }
                break;
            case false:
                PrepareStageUI.Instance.AddCrystal(1);
                break;
        }
    }

}
