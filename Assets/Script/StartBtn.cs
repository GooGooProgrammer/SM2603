using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartBtn : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Instance.SetGameState(GameState.Fight);;
        EnemyControl.Instance.StartWave();
        transform.parent.gameObject.SetActive(false);
    }
}
