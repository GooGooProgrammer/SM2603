using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed;

    Animator animator;
    public static Player Instance;

    [SerializeField]
    float jumpingHeight;
    public Transform Weapon;

    bool jumpAvailable = true;

    void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        Vector3 currentPos = transform.position;
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(
                currentPos.x - speed * Time.deltaTime,
                currentPos.y,
                currentPos.z
            );
            GetComponent<SpriteRenderer>().flipX = true;
            animator.SetBool("walking", true);
            //Weapon.localPosition = new Vector3(1,2.5f,0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(
                currentPos.x + speed * Time.deltaTime,
                currentPos.y,
                currentPos.z
            );
            GetComponent<SpriteRenderer>().flipX = false;
            animator.SetBool("walking", true);
            //Weapon.localPosition = new Vector3(-1,2.5f,0);
        }
        else
        {
            animator.SetBool("walking", false);
        }
    }
    void Jump()
    {
        if (!jumpAvailable) return;

        if (Input.GetKey(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpingHeight;
            jumpAvailable = false;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Border"))
        {
            Debug.Log("11");
            if (other.contacts[0].normal == Vector2.up)
            {
                Debug.Log("22");
                jumpAvailable = true;
            }
        }
    }
}
