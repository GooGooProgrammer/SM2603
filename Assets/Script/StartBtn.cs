using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartBtn : MonoBehaviour, IPointerDownHandler
{
    
    [SerializeField]
    Image image;
    bool inFight = false;

	public void OnPointerDown (PointerEventData eventData) 
	{
		        if (inFight) return;
        EnemyControl.Instance.StartWave();
        gameObject.SetActive(false);
	}
}
