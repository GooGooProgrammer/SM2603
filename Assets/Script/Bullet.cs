using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed{private get;set;}
    public int damage{private get;set;}
    public LayerMask bulletBlocker{private get;set;}
    Rigidbody2D rb;

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        SetStraightVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetStraightVelocity()
    {
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageble iDamageable = collision.gameObject.GetComponent<IDamageble>();

        if(iDamageable!=null)
        {
            iDamageable.Damage(damage);
        }

        if((bulletBlocker.value & (1<< collision.gameObject.layer))>0)
        {
            Destroy(gameObject);
        }
    }
}
