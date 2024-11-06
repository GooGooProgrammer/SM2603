using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellControl : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        CastSpell();
    }

    void CastSpell()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            transform.GetChild(0).GetComponent<Spell>().CastSpell();
            CancelSpell();
            transform.GetChild(0).GetComponent<Spell>().PreCast();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            transform.GetChild(1).GetComponent<Spell>().CastSpell();
            CancelSpell();
            transform.GetChild(1).GetComponent<Spell>().PreCast();
        }
        if (Input.GetMouseButtonDown(1))
        {
            CancelSpell();
        }
    }

    void CancelSpell()
    {
        foreach (Transform spell in transform)
        {
            spell.GetComponent<Spell>().CancelCast();
        }
    }
}
