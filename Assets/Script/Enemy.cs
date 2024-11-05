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
    float speed;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.position += new Vector3(speed, 0);
    }

    public void Damage(int damage)
    {
        animator.SetTrigger("OnHit");
        hp = hp - damage;
        CheckDead();
    }

    private void CheckDead()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
