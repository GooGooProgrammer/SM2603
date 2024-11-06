using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [SerializeField]
    protected float spellCD;

    [SerializeField]
    protected int checkFrequency;

    [SerializeField]
    protected float duration;

    [SerializeField]
    protected int damage;

    protected bool onCoolDown = false;
    protected Collider2D col;
    protected SpriteRenderer spriteRenderer;
    protected List<Transform> EnemyList = new List<Transform>();

    // Start is called before the first frame update'
    private void Awake()
    {
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        SpellAreaFollowMouse();
    }

    public virtual void CastSpell()
    {
        if (onCoolDown)
            return;
        if (!spriteRenderer.enabled)
            return;
        //CastingSpell = Instantiate(TheSpell, SpellArea.transform.position, Quaternion.identity);
        onCoolDown = true;
        StartCoroutine(CoolDownCalculate());
        StartCoroutine(Casting());
    }

    public void PreCast()
    {
        if (onCoolDown)
            return;
        spriteRenderer.enabled = true;
    }

    public void CancelCast()
    {
        if (col.enabled)
            return;
        spriteRenderer.enabled = false;
    }

    protected virtual void SpellAreaFollowMouse()
    {
        if (col.enabled)
            return;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }

    protected IEnumerator CoolDownCalculate()
    {
        // a cool down animation should be calculated
        yield return new WaitForSeconds(spellCD);
        onCoolDown = false;
    }

    protected IEnumerator Casting()
    {
        spriteRenderer.enabled = true;
        col.enabled = true;
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < checkFrequency; i++)
        {
            Debug.Log(EnemyList.Count);
            Effect();
            yield return new WaitForSeconds(duration / checkFrequency);
        }
        spriteRenderer.enabled = false;
        col.enabled = false;
        EnemyList = new List<Transform>();
    }

    protected abstract void Effect();

    void OnTriggerEnter2D(Collider2D col)
    {
        IDamageble iDamageable = col.gameObject.GetComponent<IDamageble>();
        if (iDamageable == null)
            return;
        EnemyList.Add(col.transform);
    }
}
