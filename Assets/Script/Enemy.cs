using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,IDamageble
{
    // Start is called before the first frame update
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Damage(int damgae)
    {
        animator.SetTrigger("OnHit");
    }
}
