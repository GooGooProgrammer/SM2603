using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCoolDown : MonoBehaviour
{
    [SerializeField] Image coolDown;
    // Start is called before the first frame update

    public void UpdateCoolDown(int angle)
    {
        angle = 360 - angle;
        coolDown.fillAmount = (float)angle / 360;
    }
}
