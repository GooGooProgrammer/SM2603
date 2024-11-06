using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed;

    public static Player Instance;

    void Awake()
    {
        Instance = this;
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
            transform.position = new Vector3(currentPos.x - speed, currentPos.y, currentPos.z);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(currentPos.x + speed, currentPos.y, currentPos.z);
        }
        Camera.main.transform.GetComponent<CameraControl>().FollowThePlayer(transform.position.x);
    }
}
