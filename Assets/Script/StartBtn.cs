using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartBtn : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    Image image;
    bool inFight = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (inFight)
            return;
        EnemyControl.Instance.StartWave();
        gameObject.SetActive(false);
    }
}
