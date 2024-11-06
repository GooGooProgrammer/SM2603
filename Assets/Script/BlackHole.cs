using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlackHole : Spell
{
    protected override void Effect()
    {
        foreach (Transform enemy in EnemyList)
        {
            if (enemy)
                enemy.position = transform.position;
        }
    }
}
