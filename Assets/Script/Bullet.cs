using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed { private get; set; }
    public int damage { private get; set; }
    public LayerMask bulletBlocker { private get; set; }
    Rigidbody2D rb;
    Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("InAir",true);
        SetStraightVelocity();
        transform.eulerAngles += new Vector3(0,0,-90);

    }

    // Update is called once per frame
    void Update() { }

    void SetStraightVelocity()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageble iDamageable = collision.gameObject.GetComponent<IDamageble>();

        if (iDamageable != null)
        {
            iDamageable.Damage(damage);
        }

        if ((bulletBlocker.value & (1 << collision.gameObject.layer)) > 0)
        {
            animator.SetBool("InAir",false);
            rb.velocity= Vector2.zero;
            GetComponent<Collider2D>().enabled = false;
        }
    }
    private void DestroyItself()
    {
        Destroy(gameObject);
    }
}
