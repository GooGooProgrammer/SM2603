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

    void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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
        }
        else
        {
            animator.SetBool("walking", false);
        }
        Camera.main.transform.GetComponent<CameraControl>().FollowThePlayer(transform.position.x);
    }
}
