using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageble
{
    // Start is called before the first frame update
    Animator animator;

    [SerializeField]
    int hp;
    [SerializeField]    
    SpriteRenderer whiteSilhouette;
    public float speed;

    [SerializeField]
    private int power;

    [SerializeField]
    private int id;

    public int GetId()
    {
        return id;
    }

    public int GetPower()
    {
        return power;
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0);
        GetComponent<SpriteMask>().sprite = GetComponent<SpriteRenderer>().sprite;
        GetComponent<SpriteRenderer>().material = GetComponent<SpriteRenderer>().material;
    }

    public void Damage(int damage)
    {
        if (hp <= 0)
            return;
        animator.SetTrigger("OnHit");
        hp = hp - damage;
        CheckDead();
    }

    private void CheckDead()
    {
        if (hp <= 0)
        {
            EnemySetControl.Instance.EnemyNumberDecreaseOne(id);
            Destroy(gameObject);
        }
    }
}
