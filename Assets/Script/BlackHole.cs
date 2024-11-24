using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BlackHole : Spell
{
    void OnTriggerEnter2D(Collider2D col)
    {
        IDamageble iDamageable = col.gameObject.GetComponent<IDamageble>();
        if (iDamageable == null)
            return;
        EnemyList.Add(col.transform);
        col.gameObject.GetComponent<Enemy>().speed =
            col.gameObject.GetComponent<Enemy>().speed / 10;
    }

    protected override void EndCasting()
    {
        foreach (Transform enemy in EnemyList)
        {
            if (enemy)
            {
                enemy.GetComponent<Enemy>().speed = enemy.GetComponent<Enemy>().speed * 10;
            }
        }
    }
        protected override void Upgrade(int i , bool isOn)
    {
        switch ((i,isOn))
        {
            case (0,true):
            transform.localScale = Vector3.one * 20;
            checkFrequency = 4;
            duration = 4;
                break;
            case (0,false):
            transform.localScale = Vector3.one * 10;
            checkFrequency=3;
            duration = 3;
                break;              
        }
    }
}
