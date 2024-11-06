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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.GetChild(0).GetComponent<Spell>().CastSpell();
            transform.GetChild(0).GetComponent<Spell>().PreCast();
        }
        if (Input.GetMouseButtonDown(1))
        {
            foreach (Transform spell in transform)
            {
                spell.GetComponent<Spell>().CancelCast();
            }
        }
    }
}
