using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject weapon;

    private Vector2 direction;
    [SerializeField]
    float speed;
    // Start is called before the first frame update\
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        Move();
        HandleWeaponRotation();
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
    }
    void HandleWeaponRotation()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos-(Vector2)weapon.transform.position).normalized;
        weapon.transform.right = direction; 
    }
}
