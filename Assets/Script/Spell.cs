using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public abstract class Spell : MonoBehaviour, IUpgradeAble
{
    [SerializeField]
    protected float spellCD;

    [SerializeField]
    protected int checkFrequency;

    [SerializeField]
    protected float duration;

    [SerializeField]
    protected int damage;

    [SerializeField]
    protected SpellCoolDown spellCoolDown;

    [SerializeField]
    protected List<Toggle> toggles;

    protected Animator animator;
    protected bool onCoolDown = false;
    protected Collider2D col;
    protected SpriteRenderer spriteRenderer;
    protected List<Transform> EnemyList = new List<Transform>();

    // Start is called before the first frame update'
    private void Awake()
    {
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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
        spriteRenderer.material.color = Color.grey;
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
        for (int i = 0; i <= 360; i++)
        {
            spellCoolDown.UpdateCoolDown(i);
            yield return new WaitForSeconds(spellCD / 360);
        }

        onCoolDown = false;
    }

    protected virtual IEnumerator Casting()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.material.color = Color.white;

        col.enabled = true;
        animator.SetTrigger("Cast");
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < checkFrequency; i++)
        {
            Debug.Log(EnemyList.Count);
            Effect();
            yield return new WaitForSeconds(duration / checkFrequency);
        }
        EndCasting();
        spriteRenderer.enabled = false;
        col.enabled = false;
        EnemyList = new List<Transform>();
    }

    protected virtual void EndCasting() { }

    protected virtual void Effect()
    {
        foreach (Transform enemy in EnemyList)
        {
            if (enemy)
                enemy.GetComponent<IDamageble>().Damage(damage);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        IDamageble iDamageable = col.gameObject.GetComponent<IDamageble>();
        if (iDamageable == null)
            return;
        EnemyList.Add(col.transform);
    }

    public void CheckUpgrade()
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            Upgrade(i , toggles[i].isOn);        
        }
    }
    protected virtual void Upgrade(int i , bool isOn)
    {

    }
}
