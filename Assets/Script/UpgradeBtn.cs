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
    [SerializeField]
    List<GameObject> upgradeItems;
    public void OnPointerDown(PointerEventData eventData)
    {
        UpgradeWindow.gameObject.SetActive(!UpgradeWindow.gameObject.activeSelf);
        StartBtn.gameObject.SetActive(!UpgradeWindow.gameObject.activeSelf);
        foreach(GameObject u in upgradeItems)
        {
            u.GetComponent<IUpgradeAble>().CheckUpgrade();
        }
    }
}
