using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : Spell
{
    protected override void SpellAreaFollowMouse()
    {
        if (col.enabled)
            return;
        transform.position = Player.Instance.transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.right = -(mousePos - (Vector2)Player.Instance.transform.position).normalized;
    }
}
