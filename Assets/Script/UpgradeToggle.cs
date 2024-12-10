using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeToggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Toggle toggle;
    [SerializeField]
    TextMeshProUGUI textMeshPro;
    void Start()
    {
        toggle.onValueChanged.AddListener(Upgrade);
    }
    void Upgrade(bool boolean)
    {
        switch (boolean)
        {
            case true:
                if(PrepareStageUI.Instance.ReduceOneCrystal() == false)
                {
                    toggle.isOn = false;
                }
                break;
            case false:
                PrepareStageUI.Instance.AddCrystal();
                break;
        }
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        textMeshPro.enabled = true;
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        textMeshPro.enabled = false;
    }
}
