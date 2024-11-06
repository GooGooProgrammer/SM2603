using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField]
    float spellCD;

    [SerializeField]
    GameObject TheSpell;

    [SerializeField]
    int checkFrequency;

    [SerializeField]
    float duration;

    GameObject SpellArea;
    GameObject CastingSpell;
    bool onCoolDown = false;

    // Start is called before the first frame update
    private void Update()
    {
        SpellAreaFollowMouse();
    }

    public void CastSpell()
    {
        if (onCoolDown)
            return;
        if (!SpellArea)
            return;
        CastingSpell = Instantiate(TheSpell, SpellArea.transform.position, Quaternion.identity);
        Destroy(SpellArea);
        onCoolDown = true;
        StartCoroutine(CoolDownCalculate());
        StartCoroutine(Casting());
    }

    public void PreCast()
    {
        if (onCoolDown)
            return;
        if (SpellArea)
            return;
        SpellArea = Instantiate(TheSpell);
        SpellArea.GetComponent<Collider2D>().enabled = false;
    }

    public void CancelCast()
    {
        if (SpellArea)
            Destroy(SpellArea);
    }

    void SpellAreaFollowMouse()
    {
        if (!SpellArea)
            return;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SpellArea.transform.position = mousePos;
    }

    IEnumerator CoolDownCalculate()
    {
        // a cool down animation should be calculated
        yield return new WaitForSeconds(spellCD);
        onCoolDown = false;
    }

    IEnumerator Casting()
    {
        for (int i = 0; i < checkFrequency; i++)
        {
            yield return new WaitForSeconds(duration / checkFrequency);
        }
        Destroy(CastingSpell);
    }
}
