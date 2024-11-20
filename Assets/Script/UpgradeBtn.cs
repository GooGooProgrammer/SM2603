using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeBtn : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    GameObject UpgradeWindow;
    [SerializeField]
    GameObject StartBtn;
    public void OnPointerDown(PointerEventData eventData)
    {
        UpgradeWindow.gameObject.SetActive(!UpgradeWindow.gameObject.activeSelf);
        StartBtn.gameObject.SetActive(!UpgradeWindow.gameObject.activeSelf);
    }
}
